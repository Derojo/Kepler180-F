using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("PlacementData", true, "")]
public class PlacementData : Singleton<PlacementData> {

    public List<BuildingNode> placementNodes;

	// Use this for initialization
	void Start () {
        if (placementNodes == null) {
            placementNodes = new List<BuildingNode>();
        }
	}

    public void AddBuildingNode(int x, int z, Types.buildingtypes type, GameObject model) {
        BuildingNode bn = new BuildingNode();
        bn.x = x;
        bn.z = z;
        bn.type = type;
        bn.model = model;
        if (!placementNodes.Contains(bn)) {
            placementNodes.Add(bn);
        }
    }

}
