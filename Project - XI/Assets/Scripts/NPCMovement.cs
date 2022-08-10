using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : TacticsMove
{
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        //Rayo para saber hacia dónde está viendo el personaje
        #region
        Debug.DrawRay(transform.position, transform.forward);
        #endregion

        //Validación del turno de la unidad
        if (!turn)
        {
            return;
        }

        //Validación para el movimiento de la unidad
        if (!moving)
        {
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();
            actualTargetTile.target = true;
        }
        else
        {
            Move();
        }
    }

    //Función para calcular cuál es camino óptimo a seguir por los NPC de acuerdo a la prioridad definida
    private void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }


    //Función que detecta el personaje más cercano al enemigo
    private void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        //Validación de la cercanía de todos los personajes
        foreach(GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);
            
            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        target = nearest;
    }
}
