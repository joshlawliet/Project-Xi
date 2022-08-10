using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Variables de estado de los Tiles
    #region
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;
    public bool poisoned = false;
    public float poisonTest;
    #endregion

    //Lista de los Tiles cercanos (necesario para los algoritmos de búsqueda)
    public List<Tile> adjacencyList = new List<Tile>();

    //Variables del algoritmo BFS(Breadth first Search)
    #region
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;
    #endregion

    //Variables para el algoritmo A*
    #region
    public float f = 0;
    public float g = 0;
    public float h = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (!walkable)
        {
            GetComponent<Renderer>().material.color = Color.gray;
        }
        else if (poisoned)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Reset(bool poison)
    {
        walkable = true;
        current = false;
        target = false;
        selectable = false;
        poisoned = poison;

        adjacencyList.Clear();

        visited = false;
        parent = null;
        distance = 0;

        f = g = h = 0;
    }

    public void FindNeighbors(float jumpHeight, Tile target, bool poison)
    {
        Reset(poison);

        CheckTile(Vector3.forward, jumpHeight, target);
        CheckTile(-Vector3.forward, jumpHeight, target);
        CheckTile(Vector3.right, jumpHeight, target);
        CheckTile(-Vector3.right, jumpHeight, target);
    }

    public void CheckTile(Vector3 direction, float jumpHeight, Tile target)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach(Collider collider in colliders)
        {
            Tile tile = collider.GetComponent<Tile>();
            if (tile != null && tile.walkable)
            {
                RaycastHit hit;

                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || (tile == target))
                {
                    adjacencyList.Add(tile);
                }
            }
        }
    }
}
