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

    public void removeBuildingNode(int x, int z, GameObject building) {
        GameObject find = null;
        if (placementNodes.Count > 1) {
            foreach (BuildingNode node in placementNodes)
            {
                if (node.x == x && node.z == z)
                {
                    placementNodes.Remove(node);
                }
                GameObject parent = Grid.getGridCellAtPosition(x, z);

                if (parent.GetComponentInChildren<SubBuilding>())
                {
                    foreach (SubBuilding sub in parent.GetComponentsInChildren<SubBuilding>())
                    {
                        if (sub.firstParent == building || sub.secondParent == building)
                        {
                            DestroyObject(sub.gameObject);
                            return;
                        }
                    }
                }

            } 
        } else {
            placementNodes.RemoveAt(0);
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
            BlueprintManager.I.bluePrintTurnsTotal = BlueprintManager.I.bluePrintTurnsTotal + model.GetComponent<BuildingType>().buildTime;
            BlueprintManager.I.bluePrintMoneyTotal = BlueprintManager.I.bluePrintMoneyTotal + model.GetComponent<BuildingType>().buildingCost;
            if (!planningNodes.Contains(bn))
            {
                planningNodes.Add(bn);
            }
            EventManager.TriggerEvent("updateplanUI");
        }

    }



}
