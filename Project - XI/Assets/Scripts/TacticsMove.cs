using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour
{
    public bool turn = false;

    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    public bool moving = false;

    //Variables para las estadísticas de los personajes
    #region
    public int health = 200;
    public int move = 4;
    public float jumpHeight = 2;
    public float moveSpeed = 2;
    public float jumpVelocity = 4.5f;
    public float resilience = .1f;
    #endregion

    //Variables para el Control de Masas
    #region 
    public bool zombified = false;
    public bool poisoned = false;
    public int poisonCD = 0;
    #endregion

    //Variables para el desplazamiento de los personajes
    #region
    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingEdge = false;
    Vector3 jumpTarget;

    public Tile actualTargetTile;
    #endregion

    protected void Init()
    {
        halfHeight = GetComponent<Collider>().bounds.extents.y;

        tiles = GameObject.FindGameObjectsWithTag("Tile");

        //Las unidades se añaden 
        TurnManager.AddUnit(this);
    }

    //Función para detectar el tile sobre el cual están paradas las unidades
    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    //Obtener el tile objetivo
    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile tile = null;

        if(Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
        }

        return tile;
    }

    public void ComputeAdjacencyLists(float jumpHeight, Tile target)
    {
        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors(jumpHeight, target, t.poisoned);
            
        }
    }

    public void FindSelectableTiles()
    {
        ComputeAdjacencyLists(jumpHeight, null);
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        //currentTile.parent = null;

        while (process.Count > 0)
        {
            Tile t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            if (t.distance < move)
            {
                foreach (Tile tile in t.adjacencyList)
                {
                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }

    public void MoveToTile(Tile tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;

        Tile next = tile;
        while(next !=null)
        {
            path.Push(next);
            next = next.parent;
        }
    } 
    
    public void Move()
    {
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;

            //Calculate the unit's position on topo of the target tile
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if(Vector3.Distance(transform.position, target)>= 0.05f)
            {
                bool jump = (transform.position.y != target.y);

                if (jump)
                {
                    Jump(target);
                }
                else
                {
                    CalculateHeading(target);
                    SetHorizontalVelocity();
                }

                //Locomotion
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                //Tile center reached
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();
            moving = false;

            TurnManager.EndTurn();
        }
    }

    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }

        foreach (Tile tile in selectableTiles)
        {
            tile.Reset(tile.poisoned);
        }

        selectableTiles.Clear();
    }

    private void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    private void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    private void Jump(Vector3 target)
    {
        if(fallingDown)
        {
            FallDownward(target);
        }
        else if (jumpingUp)
        {
            JumpUpward(target);
        }
        else if (movingEdge)
        {
            MoveToEdge(target);
        }
        else
        {
            PrepareJump(target);
        }
    }

    private void PrepareJump(Vector3 target)
    {
        float targetY = target.y;

        target.y = transform.position.y;

        CalculateHeading(target);

        if (transform.position.y > targetY)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = true;

            jumpTarget = transform.position + (target - transform.position) / 2f;
        }
        else
        {
            fallingDown = false;
            jumpingUp = true;
            movingEdge = false;

            velocity = heading * moveSpeed / 3f;

            float difference = targetY - transform.position.y;

            velocity.y = jumpVelocity * (.5f + difference / 2f);
        }
    }

    private void FallDownward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y < target.y)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = false;

            Vector3 p = transform.position;
            p.y = target.y;
            transform.position = p;

            velocity = new Vector3();

        }
    }

    private void JumpUpward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if(transform.position.y > target.y)
        {
            jumpingUp = false;
            fallingDown = true;
        }
    }

    private void MoveToEdge(Vector3 target)
    {
        if (Vector3.Distance(transform.position, jumpTarget)>= 0.05f)
        {
            SetHorizontalVelocity();
        }
        else
        {
            movingEdge = false;
            fallingDown = true;

            velocity /= 5.0f;
            velocity.y = 1.5f;
        }
    }

    protected Tile FindLowestF(List<Tile> list)
    {
        Tile lowest = list[0];

        foreach (Tile t in list)
        {
            if(t.f < lowest.f)
            {
                lowest = t;
            }
        }

        list.Remove(lowest);

        return lowest;
    }

    protected Tile FindEndTile(Tile t)
    {
        Stack<Tile> tempPath = new Stack<Tile>();

        Tile next = t.parent;
        while(next != null)
        {
            tempPath.Push(next);
            next = next.parent;
        }

        if (tempPath.Count <= move)
        {
            return t.parent;
        }

        Tile endTile = null;
        for (int n = 0; n <= move; n++)
        {
            endTile = tempPath.Pop();
        }

        return endTile;
    }


    //Función para el encontrar el camino a seguir por parte de los NPCs
    //Falta Documentar
    protected void FindPath(Tile target)
    {
        ComputeAdjacencyLists(jumpHeight, target);
        GetCurrentTile();

        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(currentTile);

        currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);
        currentTile.f = currentTile.h;

        while (openList.Count > 0)
        {
            Tile t = FindLowestF(openList);
            //Debug.DrawRay(t.transform.position, transform.up, Color.green, 1 );
            closedList.Add(t);

            if(t == target)
            {
                actualTargetTile = FindEndTile(t);
                MoveToTile(actualTargetTile);
                return;
            }

            foreach (Tile tile in t.adjacencyList)
            {
                if (!tile.walkable)
                {
                    closedList.Add(tile);
                }
                if (closedList.Contains(tile))
                {
                    //Already processed
                }
                else if (openList.Contains(tile))
                {
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if (tempG < tile.g)
                    {
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else
                {
                    tile.parent = t;

                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);
                }
            }
        }

        //TO DO: Qué hacer si no hay camino posible hacia la unidad o si ya están ocupados los espacios?
        //Sugerencia: Ejecutar el foreach anterior reduciendo en 1 el EndTile
        Debug.Log("Path not Found");
    }

    public void BeginTurn()
    {
        turn = true;
    }   
    
    public void EndTurn()
    {
        turn = false;
    }
}

