using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : TacticsMove
{
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
            FindSelectableTiles();
            CheckMouse();
        }
        else
        {
            Move();
        }    
    }


    //Funci�n para verificar la baldosa seleccionada
    void CheckMouse()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //S�lo se permite seleccionar baldosas para el movimiento
                if(hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        MoveToTile(t);
                    }
                }
            }
        }
    }
}
