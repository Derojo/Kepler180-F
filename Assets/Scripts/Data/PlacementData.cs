using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("PlacementData", true, "")]
public class PlacementData : Singleton<PlacementData> {

    public List<BuildingNode> planningNodes;
    public List<BuildingNode> placementNodes;

    public void Init() {
        if (placementNodes == null)
        {
            placementNodes = new List<BuildingNode>();
        }
        if (planningNodes == null)
        {
            planningNodes = new List<BuildingNode>();
        }
    }

    public void removeBuildingNode(int x, int z, GameObject building, bool planning = false) {

        BuildingNode removingNode = null;
        if (!planning) {
            if (placementNodes.Count > 1)
            {
                foreach (BuildingNode node in placementNodes)
                {
                    if (node.x == x && node.z == z)
                    {
                        removingNode = node;
                    }
                    placementNodes.Remove(node);
                    removeSubbuildings(x, z, building);
                    return;
                }
            }
            else
            {
                placementNodes.RemoveAt(0);
                removeSubbuildings(x, z, building);
            }
        } else
        {
            if (planningNodes.Count > 1)
            {
                foreach (BuildingNode node in planningNodes)
                {
                    if (node.x == x && node.z == z)
                    {
                        removingNode = node;
                    }
                }
                planningNodes.Remove(removingNode);
                removeSubbuildings(x, z, building);
            }
            else
            {
                planningNodes.RemoveAt(0);
                removeSubbuildings(x, z, building);
            }

        }

    }

    private void removeSubbuildings(int x, int z, GameObject building) {
        Tile parent = Grid.getTileAtPosition(x, z);
        Debug.Log(parent.subBuildings.Count);
        if (parent.subBuildings.Count > 0)
        {
            for (int i = 0; i < parent.subBuildings.Count; i++)
            {
                SubBuilding subBuilding = parent.subBuildings[i].GetComponent<SubBuilding>();
                subBuilding.secondParent.GetComponentInParent<Tile>().subBuildings.Remove(parent.subBuildings[i]);
                DestroyObject(parent.subBuildings[i]);
            }
            parent.subBuildings = new List<GameObject>();
            return;
        }
        
    }

    public void AddBuildingNode(int x, int z, Types.buildingtypes type, GameObject model, bool inPlanningMode) {
        BuildingNode bn = new BuildingNode();
        bn.x = x;
        bn.z = z;
        bn.type = type;
        bn.model = model;
        if (!inPlanningMode)
        {
            if (!placementNodes.Contains(bn))
            {
                placementNodes.Add(bn);
            }
        }
        else
        {

            if (!planningNodes.Contains(bn))
            {
                planningNodes.Add(bn);
            }
            EventManager.TriggerEvent("updateplanUI");
        }

    }



}
