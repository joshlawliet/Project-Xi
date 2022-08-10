using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<TacticsMove>> units = new Dictionary<string, List<TacticsMove>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<TacticsMove> turnTeam = new Queue<TacticsMove>();
    static bool enemyTurn = false;


    void Update()
    {
        //Revisar si ya terminó el turno de un equipo
        #region
        if (turnTeam.Count == 0)
        {
            InitTeamQueue();
        }
        #endregion
    }

    static void InitTeamQueue()
    {
        enemyTurn = !enemyTurn;
        Debug.Log(enemyTurn);
        List<TacticsMove> teamList = units[turnKey.Peek()];

        if (!enemyTurn)
        {
            Zombify(teamList);
        }

        foreach (TacticsMove unit in teamList)
        {
            if (!unit.zombified)
            {
                if (unit.poisoned)
                {
                    if (unit.poisonCD < 2)
                    {
                        unit.health -= 10;
                        unit.poisonCD += 1;
                        turnTeam.Enqueue(unit);
                        CheckTileState(unit);
                    }
                    else
                    {
                        unit.poisoned = false;
                        unit.poisonCD = 0;
                    }
                }
                else
                {
                    turnTeam.Enqueue(unit);
                    CheckTileState(unit);
                }
            }
        }
        StartTurn();
    }

    public static void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
        }
    }

    public static void EndTurn()
    {
        TacticsMove unit = turnTeam.Dequeue();
        unit.EndTurn();

        if(turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamQueue();
        }
    }

    public static void AddUnit(TacticsMove unit)
    {
        List<TacticsMove> list;

        if(!units.ContainsKey(unit.tag))
        {
            list = new List<TacticsMove>();
            units[unit.tag] = list;

            if(!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }


    //Función que lanza las ondas que zombifican a las unidades aliadas
    //Debería moverse al script de ZWaves
    private static void Zombify(List<TacticsMove> teamList)
    {
        float randomFloat;
        foreach (TacticsMove unit in teamList)
        {
            randomFloat = Random.value;
            Debug.Log(unit.name + randomFloat.ToString());
            //Si la resiliencia es menor a un número aleatorio, la unidad 
            if (unit.resilience <= randomFloat)
            {
                unit.zombified = true;
                //unit.GetComponent<NPCMovement>().health = unit.GetComponent<PlayerMovement>().health;
                //Destroy(unit.GetComponent<PlayerMovement>());

                //Para zombificar la unidad, se deshabilita el script de movimiento de unidad y habilita el movimiento por NPC
                //TO DO: Quitar el script completamente e insertar el otro
                unit.GetComponent<PlayerMovement>().enabled = false;
                unit.GetComponent<NPCMovement>().enabled = true;
                unit.tag = "NPC";
                unit.GetComponent<Renderer>().material.color = Color.grey;
            }
        }

        //Algoritmo de prueba del estado de envenenamiento en tiles
        //Se debe quitar una vez que ya funcione el envenamiento por otros modos
        #region
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            randomFloat = Random.value;
            if (t.poisonTest < randomFloat)
            {
                t.poisoned = true;
            }
        }
        #endregion
    }


    //Algoritmo que revisa si el tile tiene algún efecto de estado (quemadura, veneno, etc)
    public static void CheckTileState(TacticsMove character)
    {
        RaycastHit hit;
        if (Physics.Raycast(character.transform.position, Vector3.down, out hit, 1))
        {
            //Si el Tile está envenenado y hay una unidad sobre éste, la unidad se envenenará
            //To Do: Darle un contador a los tiles para quitarles el efecto tras ciertos turnos
            if (hit.collider.GetComponent<Tile>().poisoned)
            {
                character.poisoned = true;
            }
        }
    }
}
