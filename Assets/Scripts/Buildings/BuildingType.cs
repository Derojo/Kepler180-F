using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BuildingType : MonoBehaviour
{

    public Types.buildingtypes type;
    public bool blueprint = false;
    public float buildingCost;
    public float buildingPowerUsage;
    public bool UsingPower = false;
    public bool buildingDone = false;
    public bool turnedOn = false;
    public bool noEnergy = false;
    public bool bought = false;
    public Material boughtMaterial;
    public Material turnedOnMaterial;


    //constructing building variables
    public int buildTime;
    public int buildTimeTotal;
    // Every building needs to have a buildingInfoCanvas!
    public Canvas buildingInfoCanvas;
    private Text buildTimeInfo;
    private bool eventBuildingcall = false;

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

    void Start() {
        buildTimeTotal = buildTime;
        if (!blueprint && type != Types.buildingtypes.maingenerator) {
            if (type == Types.buildingtypes.colorgenerator) {
                buildingInfoCanvas.gameObject.SetActive(true);
            }
            buildTimeInfo = buildingInfoCanvas.transform.GetChild(1).GetComponent<Text>();
            buildTimeInfo.text = buildTime.ToString();
        }
    }

    void setNextTurn()
    {

        // Activate building build process if it's bought
        if (bought)
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
            updateBuildingInfoCanvas();
            //when building is done and event is not called, call it ONCE to reduce money ONCE
            if (buildingDone && !eventBuildingcall)
            {
                if (turnedOnMaterial != null)
                {
                    GetComponent<Renderer>().material = turnedOnMaterial;
                }

                if (type == Types.buildingtypes.colorgenerator)
                {
                    GetComponent<ColorGenerator>().turnOnGenerator();
                }

                eventBuildingcall = true;

                //Deduct total turns in blueprint
                BlueprintManager.I.bluePrintTurnsTotal = BlueprintManager.I.bluePrintTurnsTotal - buildTimeTotal;
                EventManager.TriggerEvent("updateplanUI");

            }

            if (buildingDone && !turnedOn)
            {
                turnedOn = true;
                return;
            }

            // Building is on start the right functionalities
            if (turnedOn)
            {
                if (type == Types.buildingtypes.colorgenerator)
                {
                    GetComponent<ColorGenerator>().addAuraPower();
                }
                else if (type == Types.buildingtypes.mineraldrill)
                {
                    GetComponent<SubBuilding>().harvastMinerals();
                }
                else if (type == Types.buildingtypes.energytransformer)
                {
                    GetComponent<SubBuilding>().generateConstantPower();
                }
                else if (type == Types.buildingtypes.energygenerator)
                {
                    GetComponent<SubBuilding>().generatePowerOnce();
                }
                //reduce power if building is on
                ResourceManager.I.calculatePowerLevel(buildingPowerUsage);
                EventManager.TriggerEvent("updateUI");

            }
        }

        if (gameObject.GetComponent<SubBuilding>())
        {
            // Show subbuilding, the subbuilding is not activated yet it needs to be bought first
            gameObject.GetComponent<SubBuilding>().showSubBuilding();
        }


    }

    private void updateBuildingInfoCanvas()
    {
        if (type != Types.buildingtypes.maingenerator)
        {
            buildTimeInfo.text = buildTime.ToString();
            if (buildingDone) {
                buildingInfoCanvas.transform.GetChild(0).gameObject.SetActive(false);
                buildingInfoCanvas.transform.GetChild(1).gameObject.SetActive(false);
                buildingInfoCanvas.transform.GetChild(2).gameObject.SetActive(false);
                buildingInfoCanvas.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }
}






