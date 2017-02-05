using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Color class for color specific calculations 
 */
[Prefab("ColorManager", true, "")]
public class ColorManager : Singleton<ColorManager>
{

    public List<Cluster> colorCluster;
    public List<int> sameColorAmount = new List<int>();
    public int[,] ColorsToBlend;
    public int[,] blendToColors;
    public List<ColorBlend> blends = new List<ColorBlend>();

    [System.Serializable]
    public class ColorBlend {
        public int blend;
        public int firstColor;
        public int secondColor;

        public ColorBlend(int _blend, int first, int second ) {
            blend = _blend;
            firstColor = first;
            secondColor = second;
        }
    }

    void Start() {
        if(blends.Count == 0)
        {
            Initialize();
        }

    }

    public void resetClusters() {
        colorCluster = new List<Cluster>();
        sameColorAmount = new List<int>();
    }

    private void Initialize() {
        blends.Add(new ColorBlend((int)Types.blendedColors.Purple, (int)Types.colortypes.Red, (int)Types.colortypes.Blue));
        blends.Add(new ColorBlend((int)Types.blendedColors.Brown, (int)Types.colortypes.Red, (int)Types.colortypes.Green));
        blends.Add(new ColorBlend((int)Types.blendedColors.Orange, (int)Types.colortypes.Red, (int)Types.colortypes.Yellow));
        blends.Add(new ColorBlend((int)Types.blendedColors.Lime, (int)Types.colortypes.Yellow, (int)Types.colortypes.Green));
        blends.Add(new ColorBlend((int)Types.blendedColors.Green, (int)Types.colortypes.Yellow, (int)Types.colortypes.Blue));
        blends.Add(new ColorBlend((int)Types.blendedColors.Turquoise, (int)Types.colortypes.Blue, (int)Types.colortypes.Green));
    }
    public void AddColorCluster() {
        colorCluster.Add(new Cluster());
    }

    public Types.blendedColors getBlendingColor(int colorA, int colorB) {

        if (blends.Count == 0)
            Initialize();
        Types.blendedColors returnBlending = Types.blendedColors.Null;

        foreach (ColorBlend blend in blends) {
            if (colorA == blend.firstColor && colorB == blend.secondColor || colorB == blend.firstColor && colorA == blend.secondColor) {
                return (Types.blendedColors)blend.blend;
            }
        }

        return returnBlending;
    }

    public int[] getColorsOfBlend(int incomingBlend)
    {
        Debug.Log("incblend+" + incomingBlend+"-blendcount:"+ blends.Count);
        if (blends.Count == 0)
            Initialize();
        int[] returnColors = new int[2];

        foreach (ColorBlend blend in blends)
        {
            Debug.Log(incomingBlend + "-" + blend.blend);
            if (incomingBlend == blend.blend)
            {
                returnColors[0] = blend.firstColor;
                returnColors[1] = blend.secondColor;
                return returnColors;
            }
        }

        return returnColors;
    }
    public void fillSpotInCluster(Cluster cluster, int colorId, bool mixed, Tile tile) {
        if (!mixed)
        {
            cluster.colorSpots[colorId] = true;
        }
        else {
            cluster.mixedColorSpots++;
            cluster.spotTiles.Add(tile);
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
           // bool inColorCluster = false;
           // bool inMixedCluster = false;
            int inColorCluster = 0;
            int inMixedCluster = 0;

            List<int> mixedClusterIds = new List<int>();
            int currentClusterId = -1;
            bool inNone = false;
            for (int i = 0; i < neigbours.Count; i++)
            {
                Tile neighbourTile = Grid.getTileAtPosition(neigbours[i].x, neigbours[i].z);

                if (neighbourTile.currentObject != null)
                {
                    if (!neighbourTile.inColorCluster && !neighbourTile.inMixedCluster) {
                        if(neighbourTile.currentObject.name != "MainStation")
                        {
                            inNone = true;
                        }
                    }
                    if (neighbourTile.inColorCluster )
                    {
                        inColorCluster++;
                        if (color != neighbourTile.colorCluster)
                        {
                            return false;
                        }
                    }
                    if (grid.clusterRestriction) {
                        if (neighbourTile.inMixedCluster)
                        {
                            int clusterId = neighbourTile.clusterId;
 
                            if(!mixedClusterIds.Contains(clusterId))
                            {
                                mixedClusterIds.Add(clusterId);
                            }
                           
                            
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
        if (mixedClusterIds.Count > 0 && inColorCluster > 0)
        {
            return false;
        }
        else if (mixedClusterIds.Count >= 2) {
            return false;
        }
        else if (mixedClusterIds.Count == 1 && inNone)
        {
            int combinedSpots = 1;
            for (int i = 0; i < mixedClusterIds.Count; i++)
            {
                combinedSpots = combinedSpots + ColorManager.I.colorCluster[mixedClusterIds[i]].mixedColorSpots;
            }
            if (combinedSpots >= 4)
            {
                return false;
            }
        }
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

    public List<Tile> GetAdjunctTilesWithObjects(Tile tile, GridManager grid)
    {
        List<Tile> returnTiles = new List<Tile>();
        List<Node> neigbours = grid.graph[tile.x, tile.z].neighbours;
        for (int i = 0; i < neigbours.Count; i++)
        {
            Tile neighbourTile = Grid.getTileAtPosition(neigbours[i].x, neigbours[i].z);

            if (neighbourTile.currentObject != null)
            {
                if (neighbourTile.tileType == (int)Types.buildingtypes.colorgenerator)
                {
                    returnTiles.Add(neighbourTile);
                }
            }
        }
        return returnTiles;
    }

    public List<Tile> getBlendableTiles(Tile tile, Types.colortypes color, GridManager grid)
    {
        List<Tile> returnTiles = new List<Tile>();
        int colorOfTileSearching = (int) color;
        
        List<Node> neigbours = grid.graph[tile.x, tile.z].neighbours;
        for (int i = 0; i < neigbours.Count; i++)
        {
            Tile neighbourTile = Grid.getTileAtPosition(neigbours[i].x, neigbours[i].z);
            if (neighbourTile.currentObject != null)
            {
                if (neighbourTile.tileType == (int)Types.buildingtypes.colorgenerator)
                {
                    ColorGenerator neighborGenerator = neighbourTile.currentObject.GetComponent<ColorGenerator>();
                    Types.colortypes neighbourColor = neighborGenerator.selectedColor;
                    // Add blending color to array of the tile searching for neighbor tile color blending
                    if (tile.blendedColors.Count < 4) { 
                        tile.blendedColors.Add((int)neighbourColor);
                        returnTiles.Add(neighbourTile);
                    }
                    // Add blending color of the searching tile to the neighbor tile array;
                    if (neighbourTile.blendedColors.Count < 4)
                    {
                        neighbourTile.blendedColors.Add(colorOfTileSearching);
                    }
                }
            }
        }

        return returnTiles;
    }
}