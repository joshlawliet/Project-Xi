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
        //Rayo para saber hacia d�nde est� viendo el personaje
        #region
        Debug.DrawRay(transform.position, transform.forward);
        #endregion

        //Validaci�n del turno de la unidad
        if (!turn)
        {
            return;
        }

        //Validaci�n para el movimiento de la unidad
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

    //Funci�n para calcular cu�l es camino �ptimo a seguir por los NPC de acuerdo a la prioridad definida
    private void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }


    //Funci�n que detecta el personaje m�s cercano al enemigo
    private void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        //Validaci�n de la cercan�a de todos los personajes
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
