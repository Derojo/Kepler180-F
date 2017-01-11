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

    public void BuyBuilding(BuildingType buildingInfo) {
        ResourceManager.I.fundings = ResourceManager.I.fundings - buildingInfo.buildingCost;
        BlueprintManager.I.bluePrintMoneyTotal = BlueprintManager.I.bluePrintMoneyTotal - buildingInfo.buildingCost;
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

}