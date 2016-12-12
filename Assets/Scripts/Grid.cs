using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Static container class for all grid cells on the map.
 * The grid always starts with x-position and z-position values on 0.
 */
static class Grid
{
    /**
     * Two-dimensional array containing instances of the grid cell for faster access. 
     */
    private static GameObject[,] grid;
     
     /**
      * Maximum grid width.
      */
     public static int xMax = 10;

    /**
     * Maximum grid depth.
     */
    public static int zMax = 10;

     /**
      * Initializes the container array
      */
     public static void initializeArray(int xMax, int zMax) {
         grid = new GameObject[xMax, zMax];
     }

/**
 * Returns a grid cell at given position. Faster than iterating through a one-dimensional array with comparisons.
 */
public static GameObject getGridCellAtPosition(int x, int z) {
         return grid[x, z];
     }

    public static void getGridCellByName(string name)
    {

       // return grid[x, z];
    }
    /**
     * Adds a grid cell to the container.
     */
    public static void addCell(int x, int z, GameObject cell) {
         grid[x, z] = cell;
     }
     
     public static GameObject[,] getGrid() {
         return grid;
     }
 }
