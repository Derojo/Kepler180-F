using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BuildingType : MonoBehaviour {

    public Types.buildingtypes type;
    public float buildingCost;
    public float buildingPowerUsage;
    public bool buildingDone = false;
    public bool eventBuildingcall = false;
    public bool UsingPower = false;
    //building time
    public int buildTime;
    private Text buildTimeInfo;

    //eventlistner
    void OnEnable()
    {
        EventManager.StartListening("EndTurn", setNextTurn);
    }
    //unregistering listeners for clean up
    void OnDisable()
    {
        EventManager.StopListening("EndTurn", setNextTurn);
    }

    void setNextTurn()
    { 
        
        if (!buildingDone)
        {
            buildTime--;
        }

        if (buildTime == 0)
        {
           
            buildingDone = true;
        }
        if(buildingDone &&!eventBuildingcall)
        {
            //reduce funds after building is done
            ResourceManager.I.fundings = ResourceManager.I.fundings - buildingCost;
            //trigger event to complete building effects
            EventManager.TriggerEvent("BuildingCompleted");
            eventBuildingcall = true;
        }
        //reducing power level after turn
        if(buildingDone)
        {
            //deducting power each turn
            ResourceManager.I.powerLevel = ResourceManager.I.powerLevel - buildingPowerUsage;
        }

        Debug.Log("Your powerlevel = " + ResourceManager.I.powerLevel);

    }
}

