using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BuildingType : MonoBehaviour {

    public Types.buildingtypes type;
    public float buildingCost;
    public float buildingPowerUsage;
    public bool eventBuildingcall = false;
    public bool UsingPower = false;
    public bool turnedOn = false;
   

    //constructing building variables
    public int buildTime;
    private Text buildTimeInfo;
    public bool buildingDone = false;

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
       
        //if a building is placed but not done yet, reduce building time
        if (!buildingDone)
        {
            buildTime--;
        }
        //If buildtime = zero then the building is done
        if (buildTime == 0)
        {
            buildingDone = true;
        }
        //when building is done and event is not called, call it ONCE to reduce money ONCE
        if(buildingDone &&!eventBuildingcall)
        {
            //trigger event to complete building effects
            EventManager.TriggerEvent("BuildingCompleted");
            eventBuildingcall = true;
           
        }
       
         if (buildingDone && !turnedOn)
         {
            turnedOn = true;
            return;
         }
          //If the building is turned on then it start using power.
         if (turnedOn)
         {
            ResourceManager.I.powerLevel = ResourceManager.I.powerLevel - buildingPowerUsage;
            EventManager.TriggerEvent("updateUI");
        }
        Debug.Log("Yo powahlvl = " + ResourceManager.I.powerLevel);
    }
    
}

