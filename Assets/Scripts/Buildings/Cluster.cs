using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cluster
{
    public bool isFull = false;
    public Dictionary<int, bool> colorSpots = new Dictionary<int, bool>() { { 0, false }, { 1, false }, { 2, false }, { 3, false } };
    public int mixedColorSpots = 0;

    public Cluster() {

    }
}
