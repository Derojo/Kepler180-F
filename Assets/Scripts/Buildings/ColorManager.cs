using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Color class for color specific calculations 
 */
[Prefab("ColorManager", false, "")]
public class ColorManager : Singleton<ColorManager>
{
    public List<Cluster> colorCluster;

    public void AddColorCluster() {
        colorCluster.Add(new Cluster());
    }

    public void fillSpotInCluster(Cluster cluster, int colorId, bool mixed ) {
        if (!mixed)
        {
            cluster.colorSpots[colorId] = true;
        }
        else {
            cluster.mixedColorSpots++;
            Debug.Log("spots" + cluster.mixedColorSpots);
        }
        if (checkIfClusterIsFull(cluster, mixed)) {
            Debug.Log("cluster is full");
            cluster.isFull = true;
        }
    }

    private bool checkIfClusterIsFull(Cluster cluster, bool mixed) {
        if (!mixed)
        {
            foreach (KeyValuePair<int, bool> colorSpot in cluster.colorSpots)
            {
                if (colorSpot.Value == false)
                {
                    return false;
                }
            }
        }
        else
        {
            if (cluster.mixedColorSpots != 4) {
                return false;
            }
        }

        return true;
    }
    public bool AbleToBuildColorGenerator(Tile tile, Types.colortypes color, GridManager grid) {

            List<Node> neigbours = grid.graph[tile.x, tile.z].neighbours;
            bool inColorCluster = false;
            bool inMixedCluster = false;
            for (int i = 0; i < neigbours.Count; i++)
            {
                Tile neighbourTile = Grid.getTileAtPosition(neigbours[i].x, neigbours[i].z);

                if (neighbourTile.currentObject != null)
                {
                    if (neighbourTile.inColorCluster)
                    {
                        inColorCluster = true;
                        if (color != neighbourTile.colorCluster)
                        {
                            return false;
                        }
                    }
                    if (grid.clusterRestriction) {
                        if (neighbourTile.inMixedCluster)
                        {
                            inMixedCluster = true;
                            int clusterId = neighbourTile.clusterId;
                            if (!ColorManager.I.colorCluster[clusterId].isFull)
                            {
                                if (!grid.mixedPlacements)
                                {
                                    if (ColorManager.I.colorCluster[clusterId].colorSpots[(int)color])
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    if (color == neighbourTile.currentObject.GetComponent<ColorGenerator>().selectedColor)
                                    {
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            if (inMixedCluster && inColorCluster)
                return false;
            return true;
    }

    public Tile getFirstAdjunctTile(Tile tile, GridManager grid) {
        List<Node> neigbours = grid.graph[tile.x, tile.z].neighbours;
        for (int i = 0; i < neigbours.Count; i++)
        {
            Tile neighbourTile = Grid.getTileAtPosition(neigbours[i].x, neigbours[i].z);

            if (neighbourTile.currentObject != null)
            {
                if (neighbourTile.tileType == (int)Types.buildingtypes.colorgenerator)
                {
                    return neighbourTile;
                }
            }
        }
        return null;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
