﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Color class for color specific calculations 
 */
[Prefab("ColorManager", false, "")]
public class ColorManager : Singleton<ColorManager>
{

    public List<Cluster> colorCluster;
    public int[,] blending;


    void Start() {
        blending = new int[6, 6];
        blending[(int)Types.colortypes.Red, (int)Types.colortypes.Blue] = (int)Types.blendedColors.Purple;
        blending[(int)Types.colortypes.Red, (int)Types.colortypes.Green] = (int)Types.blendedColors.Brown;
        blending[(int)Types.colortypes.Red, (int)Types.colortypes.Yellow] = (int)Types.blendedColors.Orange;
        blending[(int)Types.colortypes.Yellow, (int)Types.colortypes.Green] = (int)Types.blendedColors.Lime;
        blending[(int)Types.colortypes.Yellow, (int)Types.colortypes.Blue] = (int)Types.blendedColors.Green;
        blending[(int)Types.colortypes.Blue, (int)Types.colortypes.Green] = (int)Types.blendedColors.Turquoise;
    }

    public void AddColorCluster() {
        colorCluster.Add(new Cluster());
    }

    public Types.blendedColors getBlendingColor(int colorA, int colorB) {

        Types.blendedColors returnBlending = Types.blendedColors.Null;

        if (blending[colorA, colorB] != (int)Types.blendedColors.Null) {
            returnBlending = (Types.blendedColors) blending[colorA, colorB];
        } else if (blending[colorB, colorA] != (int)Types.blendedColors.Null) {
            returnBlending = (Types.blendedColors) blending[colorB, colorA];
        }

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