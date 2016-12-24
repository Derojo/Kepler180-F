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
    public  string[,] blending;
    public Dictionary<string[], string> blendingColors = new Dictionary<string[], string>()
    {
        { new string[] { "Red",     "Blue"  },  "Purple"},
        { new string[] { "Red",     "Green" },  "Brown"},
        { new string[] { "Red",     "Yellow"},  "Orange"},
        { new string[] { "Yellow",  "Green" },  "Lime"},
        { new string[] { "Yellow",  "Blue"  },  "Green"},
        { new string[] { "Blue",    "Green" },  "Turquoise"}
    };

    void Start() {
        blending = new string[6, 6];
        blending[(int)Types.colortypes.Red, (int)Types.colortypes.Blue] = "Purple";
        blending[(int)Types.colortypes.Red, (int)Types.colortypes.Green] = "Brown";
        blending[(int)Types.colortypes.Red, (int)Types.colortypes.Yellow] = "Orange";
        blending[(int)Types.colortypes.Yellow, (int)Types.colortypes.Green] = "Lime";
        blending[(int)Types.colortypes.Yellow, (int)Types.colortypes.Blue] = "Green";
        blending[(int)Types.colortypes.Blue, (int)Types.colortypes.Green] = "Turquoise";
    }

    public void AddColorCluster() {
        colorCluster.Add(new Cluster());
    }

    public string getBlendingColor(int colorA, int colorB) {
        
        string returnBlending = "";

        if (blending[colorA, colorB] != null) {
            returnBlending = blending[colorA, colorB];
        } else if (blending[colorB, colorA] != null) {
            returnBlending = blending[colorB, colorA];
        }
        /*
        string value = "";
        if (blendingColors.TryGetValue(combinationAB, out  value)) {
            Debug.Log("true");
            returnBlending = value;
        } else if (blendingColors.TryGetValue(combinationBA, out value))
        {
            Debug.Log("true1");
            returnBlending = (string)blendingColors[combinationBA];
        } */
        return returnBlending;
    }

    public void fillSpotInCluster(Cluster cluster, int colorId, bool mixed ) {
        if (!mixed)
        {
            cluster.colorSpots[colorId] = true;
        }
        else {
            cluster.mixedColorSpots++;
        }
        if (checkIfClusterIsFull(cluster, mixed)) {
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
}
