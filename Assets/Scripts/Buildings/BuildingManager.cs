using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("BuildingManager", true, "")]
public class BuildingManager : Singleton<BuildingManager>
{ 
    public BuildingObject[] buildingObjects;

    public BuildingObject getObjectByBlend(Types.blendedColors blendedColor)
    {
        foreach (BuildingObject o in buildingObjects) {
            if (o.blendedColor == blendedColor) {
                return o;
            }
        }
        return null;
    }

    public void BuyBuilding(BuildingType buildingInfo, bool inPlanning) {

        if (!inPlanning)
        {
            ResourceManager.I.fundings = ResourceManager.I.fundings - buildingInfo.buildingCost;
            BlueprintManager.I.bluePrintMoneyTotal = BlueprintManager.I.bluePrintMoneyTotal - buildingInfo.buildingCost;
        }
        else {
            BlueprintManager.I.bluePrintTurnsTotal = BlueprintManager.I.bluePrintTurnsTotal + buildingInfo.buildTime;
            BlueprintManager.I.bluePrintMoneyTotal = BlueprintManager.I.bluePrintMoneyTotal + buildingInfo.buildingCost;
        }
        buildingInfo.bought = true;
        if (buildingInfo.boughtMaterial != null) {
            buildingInfo.gameObject.GetComponent<Renderer>().material = buildingInfo.boughtMaterial;
        }
        EventManager.TriggerEvent("updateUI");
        EventManager.TriggerEvent("updateplanUI");
    }

    public bool AbleToBuy(BuildingType buildingInfo) {
        float calc = ResourceManager.I.fundings - buildingInfo.buildingCost;
        if (calc >= 0) {
            return true;
        }
        return false;
    }

    public void RemoveBuilding(GameObject building, GridManager gridManager, bool planning = false)
    {
        BuildingType buildingInfo = building.GetComponent<BuildingType>();
        if (planning) {
            BlueprintManager.I.bluePrintTurnsTotal = BlueprintManager.I.bluePrintTurnsTotal - buildingInfo.buildTime;
            BlueprintManager.I.bluePrintMoneyTotal = BlueprintManager.I.bluePrintMoneyTotal - buildingInfo.buildingCost;
            EventManager.TriggerEvent("updateplanUI");
        }
        Tile tile = building.GetComponentInParent<Tile>();
        tile.currentObject = null;
        tile.tileType = 0;
        PlacementData.I.removeBuildingNode(tile.x, tile.z, building, planning);
        giveBackMoneyAndAction(buildingInfo);
        if (tile.inMixedCluster)
        {
            tile.inMixedCluster = false;
            ColorManager.I.colorCluster[tile.clusterId].mixedColorSpots--;
            if(ColorManager.I.colorCluster[tile.clusterId].spotTiles.Contains(tile))
            {
                ColorManager.I.colorCluster[tile.clusterId].spotTiles.Remove(tile);
                ColorManager.I.colorCluster[tile.clusterId].isFull = false;
            }
            
            if (ColorManager.I.colorCluster[tile.clusterId].mixedColorSpots == 1)
            {
                Tile neighbor = ColorManager.I.getFirstAdjunctTile(tile, gridManager);
                neighbor.inMixedCluster = false;
                ColorManager.I.colorCluster.RemoveAt(tile.clusterId);
            }
        }
        else if (tile.inColorCluster)
        {
            ColorManager.I.sameColorAmount[tile.clusterId]--;
            tile.colorClusterAmount--;
            if (ColorManager.I.sameColorAmount[tile.clusterId] == 1)
            {
                Tile neighbor = ColorManager.I.getFirstAdjunctTile(tile, gridManager);
                neighbor.inColorCluster = false;
                ColorManager.I.sameColorAmount.RemoveAt(tile.clusterId);

            }
            tile.inColorCluster = false;
        }


        GameObject.Destroy(building);
        return;

    }

    public void giveBackMoneyAndAction(BuildingType buildingInfo)
    {
        ResourceManager.I.fundings = ResourceManager.I.fundings + buildingInfo.buildingCost;
        if(TurnManager.I.placementsDone != 0)
        {
            TurnManager.I.placementsDone = TurnManager.I.placementsDone - 1;
        }

        EventManager.TriggerEvent("updateUI");

    }

}