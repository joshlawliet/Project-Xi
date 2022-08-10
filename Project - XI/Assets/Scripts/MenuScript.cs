using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuScript 
{
    //Creación de un script en la barra "Tools" de Unity para asignarle un material a todos los tiles de la escena
    #region
    [MenuItem("Tools/Assign Tile Material")]
    
    public static void AssignTileMaterial()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        Material material = Resources.Load<Material>("Tile");

        foreach (GameObject t in tiles)
        {
            t.GetComponent<Renderer>().material = material;
        }
    }
    #endregion


    //Creación de un script en la barra "Tools" de Unity para asignarle un script a todos los tiles de la escena
    #region
    [MenuItem("Tools/Assign Tile Script")]

    public static void AssignTileScript()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject t in tiles)
        {
            t.AddComponent<Tile>();
        }
    }
    #endregion


    //Creación de un script en la barra "Tools" de Unity para asignarle el valor de resiliencia al veneno a todos los tiles de la escena
    //SE DEBE ELIMINAR YA QUE LOS TILES NO DEBERÍAN TENER ESTE VALOR
    //SE UTILIZA PARA REALIZAR PRUEBAS DE ENVENENAMIENTO
    #region
    [MenuItem("Tools/Assign Resilience")]

    public static void AssignTileResilience()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject t in tiles)
        {
            Tile tile = t.GetComponent<Tile>();
            tile.poisonTest = 1;
        }
    }
    #endregion
}
