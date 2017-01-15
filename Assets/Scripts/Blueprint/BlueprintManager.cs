using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Prefab("BlueprintManager", true, "")]
public class BlueprintManager : Singleton<BlueprintManager>
{
    public bool inBluePrint = false;
    public float bluePrintTurnsTotal;
    public float bluePrintMoneyTotal;



    public void ActivateBlueprint() {
        // set trigger for listeners
        EventManager.TriggerEvent("InBlueprint");
    }
    public void DeactivateBlueprint()
    {
        // set trigger for listeners
        EventManager.TriggerEvent("OutBlueprint");
    }

    public void InitializeBlueprintModels()
    {
        // Place all the models back to where the player has put them in the blueprint scene
        foreach (BuildingNode node in PlacementData.I.planningNodes)
        {
            // get the right tile according to x, z values from node
            Tile tile = Grid.getGridCellAtPosition(node.x, node.z).GetComponent<Tile>();
            // Instantiate the right model according to gameobject in node
            GameObject building = Instantiate(node.model) as GameObject;

            building.name = "bp-" + node.model.name;
            building.transform.parent = tile.transform;
            building.transform.position = tile.transform.position;

            // Deactivate mesh renderer, we don't want to see it on default
            building.GetComponent<MeshRenderer>().enabled = false;
            foreach (Transform child in building.transform)
            {
                child.gameObject.SetActiveRecursively(false);
            }
        }
    }

}
