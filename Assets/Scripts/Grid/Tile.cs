using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public int x;
    public int z;
    public int tileType;
    public bool inRange = false;
    public bool locked = false;
    public GameObject currentObject;

    public bool inColorCluster = false;
    public bool inMixedCluster = false;
    public int clusterId;
    public int colorClusterAmount = 0;
    public Types.colortypes colorCluster;
    public List<int> blendedColors;
    public List<GameObject> subBuildings = new List<GameObject>();

    void Awake() {
        blendedColors = new List<int>();
    }

}
