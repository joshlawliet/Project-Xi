using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCamera : MonoBehaviour
{

    //Rotar la c�mara a la izquiera 90�
    #region
    public void RotateLeft()
    {
       transform.Rotate(Vector3.up, 90, Space.Self);    
    }
    #endregion


    //Rotar la c�mara a la derecha 90�
    #region
    public void RotateRight()
    {
       transform.Rotate(Vector3.up, -90, Space.Self);
    }
    #endregion
}
