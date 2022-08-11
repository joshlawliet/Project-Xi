using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<TacticsMove>> units = new Dictionary<string, List<TacticsMove>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<TacticsMove> turnTeam = new Queue<TacticsMove>();
    public static bool enemyTurn = false;

  private static ZWaves zwaves = new ZWaves();
  
    #region Variables for Menu Tactics
    [SerializeField] GameObject[] menuTactics;
  #endregion

  #region Instance of GameObject array (Menu Tactics objects)
  private void Awake()
  {
    menuTactics = new GameObject[2];
  }
  #endregion

  #region Setting Tactics Menu Objects in array
  // Start is called before the first frame update
  void Start()
  {
    menuTactics[0] = GameObject.Find("ButtonTactics");
    menuTactics[1] = GameObject.Find("MenuTactics");
  }
  #endregion

  // Update is called once per frame
  void Update()
    {
        if(turnTeam.Count == 0)
        {
            InitTeamQueue();
        }
    #region Menu Tactics On and Off, according to Active Turn.
    if (enemyTurn == true)
        {
          menuTactics[0].SetActive(false);
          menuTactics[1].SetActive(false);
        }
        else
        {
          menuTactics[0].SetActive(true);
        }
    #endregion
  }

  static void InitTeamQueue()
    {
        enemyTurn = !enemyTurn;
        Debug.Log(enemyTurn);
        List<TacticsMove> teamList = units[turnKey.Peek()];

        foreach(TacticsMove unit in teamList)
        {
            turnTeam.Enqueue(unit);
        }
        StartTurn();

        if (enemyTurn)
        {
            zwaves.unleashWaves();
        }
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
  #region Menu Tactics Behavior On and Off
  public void SetOnandOff()
  {
    switch (menuTactics[1].activeSelf)
    {
      case true:
        menuTactics[1].SetActive(false);
        break;
      case false:
        menuTactics[1].SetActive(true);
        break;
    }

  }
  #endregion
}
