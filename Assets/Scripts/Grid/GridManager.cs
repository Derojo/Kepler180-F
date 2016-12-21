using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public Node[,] graph;
    public int sizeGridX;
    public int sizeGridZ;
    public Transform baseGrid;
    public GameObject tileHolder;
    public GameObject energyHolder;
    public bool gridReady = false;
    private Vector3 tileSize = Vector3.zero;
    private Vector3 gridPosition = Vector3.zero;
    private Vector3 startGridPosition = Vector3.zero;

    void Start()
    {
        Grid.xMax = sizeGridX;
        Grid.zMax = sizeGridZ;
        InitPositions();
        BuildGrid();
        GeneratePathfindingGraph();
    }

    public void BuildGrid()
    {
        Grid.initializeArray(Grid.xMax, Grid.zMax);
        for (int x = 0; x < Grid.xMax; x++)
        {
            for (int z = 0; z < Grid.zMax; z++)
            {
                GameObject tile = Instantiate(tileHolder) as GameObject;
                tile.GetComponent<Renderer>().material.renderQueue = 4000;
                tile.transform.localScale = tileSize;
                tile.transform.position = gridPosition;
                tile.name = "" + x + "|" + z + "";
                if (z >= (Grid.zMax - 1))
                    gridPosition.z = startGridPosition.z;
                else
                    gridPosition.z -= tileSize.z;
                //  gridPosition.y = 
                tile.transform.parent = transform;
                tile.SetActive(true);
                tile.GetComponent<Tile>().x = x;
                tile.GetComponent<Tile>().z = z;
                Grid.addCell(x, z, tile);
            }
            gridPosition.x -= tileSize.x;
        }
        gridReady = true;
    }

    // Set positions based on basegrid size aka GridFloor
    private void InitPositions()
    {
        // Set the size of the tile according to scale ratio of the basegrid and Grid x/z values;
        tileSize.x = baseGrid.transform.localScale.x / Grid.xMax;
        tileSize.z = baseGrid.transform.localScale.z / Grid.zMax;
        tileSize.y = 0.1f;

        gridPosition.x = (baseGrid.transform.localScale.x / 2) - (tileSize.x / 2);
        gridPosition.z = (baseGrid.transform.position.z) + (baseGrid.transform.localScale.z / 2) - (tileSize.z / 2);
        gridPosition.y = 0.2f;

        startGridPosition = gridPosition;
    }

    public void Destroy()
    {
        
        Tile[] tiles = GetComponentsInChildren<Tile>();

        for (int i = 0; i < tiles.Length; i++)
        {
            Destroy(tiles[i].gameObject);
        }
    }


    // Storing neighbour tiles
    public void GeneratePathfindingGraph()
    {
        graph = new Node[sizeGridX, sizeGridZ];

        for (int x = 0; x < sizeGridX; x++)
        {
            for (int z = 0; z < sizeGridZ; z++)
            {
                graph[x, z] = new Node();
                graph[x, z].x = x;
                graph[x, z].z = z;
            }
        }

        for (int x = 0; x < sizeGridX; x++)
        {
            for (int z = 0; z < sizeGridZ; z++)
            {
                if (x > 0)
                {
                    graph[x, z].neighbours.Add(graph[x - 1, z]);
                    if (z > 0)
                        graph[x, z].neighbours.Add(graph[x - 1, z - 1]);
                    if (z < sizeGridZ - 1)
                        graph[x, z].neighbours.Add(graph[x - 1, z + 1]);
                }

                // Try Right
                if (x < sizeGridX - 1)
                {
                    graph[x, z].neighbours.Add(graph[x + 1, z]);
                    if (z > 0)
                        graph[x, z].neighbours.Add(graph[x + 1, z - 1]);
                    if (z < sizeGridZ - 1)
                        graph[x, z].neighbours.Add(graph[x + 1, z + 1]);
                }

                // Try straight up and down
                if (z > 0)
                    graph[x, z].neighbours.Add(graph[x, z - 1]);
                if (z < sizeGridZ - 1)
                    graph[x, z].neighbours.Add(graph[x, z + 1]);
            }
        }
    }


    public void updateRange(int _x, int _z, int range)
    {
        int startX = (_x - range > 0 ? _x - range : 0);
        int startZ = (_z - range > 0 ? _z - range : 0);
        int totalIteration = ((range * 2) + 1 > Grid.zMax ? Grid.zMax : (range * 2) + 1);
        for (int x = 0; x < totalIteration; x++)
        {
            for (int z = 0; z < totalIteration; z++)
            {
                Tile t = Grid.getGridCellAtPosition(startX+x,startZ+z).GetComponent<Tile>();
                GameObject energyGround = Instantiate(energyHolder) as GameObject;

                energyGround.transform.parent = t.transform;
                energyGround.transform.position = new Vector3(t.transform.position.x, 30f, t.transform.position.z);
                energyGround.transform.localScale = energyHolder.transform.localScale;

                t.inRange = true;
            }
        }
    }
}