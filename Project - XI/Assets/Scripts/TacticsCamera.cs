using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCamera : MonoBehaviour
{

    //Rotar la cámara a la izquiera 90°
    #region
    public void RotateLeft()
    {
       transform.Rotate(Vector3.up, 90, Space.Self);    
    }
    #endregion


    //Rotar la cámara a la derecha 90°
    #region
    public void RotateRight()
    {
       transform.Rotate(Vector3.up, -90, Space.Self);
    }
    #endregion
}
