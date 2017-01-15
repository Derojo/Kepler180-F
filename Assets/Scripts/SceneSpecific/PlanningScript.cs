using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlanningScript : MonoBehaviour {

    //Ingame text
    public Text totalFunding;
    public Text totalPower;
    public UIManager uimanager;
    public Text totalPlanningCost;
    public Text totalTurnCostText;
    public Text currentTurnCostText;
    public Image AuraColorIMG;
    // Goals box
    public Animator goals;
    public Text currentLevel;
    public Text auraGoal;
    public Text powerAuraText;
    //infopanel 
    public Text buildingName;
    public Text buildingTime;
    public Text powerInput;
    public Text buildingCost;
    public Text buildingState;
    public GameObject deleteButton;
    public GameObject infoPopUp;
    public GridManager gridManager;
    private GameObject currentObject;


    //eventlistner
    void OnEnable()
    {
        EventManager.StartListening("updateplanUI", UpdatePlanningUI);
    }
    //unregistering listeners for clean up
    void OnDisable()
    {
        EventManager.StopListening("updateplanUI", UpdatePlanningUI);
    }
    // Use this for initialization
    void Start ()
    {
        Manager.I.Load();
        // Set level information
        currentLevel.text = "Level " + LevelManager.I.currentLevel;
        auraGoal.text = "= " + AuraManager.I.A_C;
        powerAuraText.text = LevelManager.I.A_P_T.ToString();
        totalFunding.text = ResourceManager.I.fundings.ToString();
        totalPower.text = ResourceManager.I.powerLevel.ToString();
        //loading/diplaying turn info
        currentTurnCostText.text = BlueprintManager.I.bluePrintTurnsTotal.ToString() + "/" + LevelManager.I.M_T_A.ToString();
        //loading/showing aura color Goals
        string[] rgba = AuraManager.I.A_C_C.Split(new string[] { "," }, StringSplitOptions.None);
        byte r = byte.Parse(rgba[0]);
        byte g = byte.Parse(rgba[1]);
        byte b = byte.Parse(rgba[2]);
        byte a = byte.Parse(rgba[3]);
        AuraColorIMG.color = new Color32(r, g, b, a);

    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitObject;

        if (Physics.Raycast(ray, out hitObject))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hitObject.collider.GetComponent<BuildingType>())
                {
                    infoPopUp.SetActive(true);
                    UpdateBuildingInfoUI(hitObject.collider.gameObject);

                }
            }
        }//end rayccast

    }
    // Update is called once per frame
    void UpdatePlanningUI ()
    {
        currentTurnCostText.text = BlueprintManager.I.bluePrintTurnsTotal.ToString() + "/" + LevelManager.I.M_T_A.ToString();
        totalPlanningCost.text = BlueprintManager.I.bluePrintMoneyTotal.ToString();
    }

    public void acceptPlanning(string changeToScene) {
        ColorManager.I.resetClusters();
        Manager.I.ChangeToScene(changeToScene);
    }

    public void UpdateBuildingInfoUI(GameObject building)
    {

        if (currentObject != building || currentObject == null)
        {
            currentObject = building;
        }
        infoPopUp.SetActive(true);
        Text infoBox = infoPopUp.transform.GetChild(8).transform.GetChild(0).GetComponent<Text>();
        if (infoPopUp)
        {

            // Every incoming building has a buildingType
            BuildingType buildingType = building.GetComponent<BuildingType>();

            // Set general information
            buildingTime.text = buildingType.buildTime.ToString();
            powerInput.text = "- " + buildingType.buildingPowerUsage.ToString();
            buildingCost.text = "- " + buildingType.buildingCost.ToString();
            // Set information according to buildingtype
            if (buildingType.type == Types.buildingtypes.colorgenerator) // Information for colorgenerators
            {
                ColorGenerator cgenerator = building.GetComponent<ColorGenerator>();

                if (cgenerator.selectedColor == Types.colortypes.Red)
                {
                    buildingName.text = "Rode Generator";
                }
                else if (cgenerator.selectedColor == Types.colortypes.Blue)
                {
                    buildingName.text = "Blauwe Generator";
                }
                //green
                else if (cgenerator.selectedColor == Types.colortypes.Green)
                {
                    buildingName.text = "Groene Generator";
                }
                //yellow
                else if (cgenerator.selectedColor == Types.colortypes.Yellow)
                {
                    buildingName.text = "Gele Generator";
                }
                // Set rest of information
                infoBox.text = "Levert " + cgenerator.buildingAuraPower.ToString() + " aurakracht per beurt";

                if (buildingType.bought)
                {
                    if (buildingType.buildTime == buildingType.buildTimeTotal)
                    {
                        deleteButton.SetActive(true);
                    }
                    else
                    {
                        deleteButton.SetActive(false);
                    }
                }
                // Show delete button if needed
            }
            else if (building.GetComponent<SubBuilding>()) // Set information for all subbuildings
            {
                deleteButton.SetActive(false);
                SubBuilding sub = building.GetComponent<SubBuilding>();
                // General
                if (buildingType.type == Types.buildingtypes.mineraldrill)  // Set information for the mineraldrill
                {
                    buildingName.text = "Mineraalboor";
                    infoBox.text = "Boort " + sub.harvestPower.ToString() + " mineralen per beurt";
                }
                else if (buildingType.type == Types.buildingtypes.energygenerator)  // Set information for the energygenerator
                {
                    buildingName.text = "Energiegenerator";
                    infoBox.text = "Genereerd (!) eenmalig" + sub.energyPowerOnce.ToString() + " energie";
                }
                else if (buildingType.type == Types.buildingtypes.energytransformer)  // Set information for the energytransformer
                {
                    buildingName.text = "Energietransformer";
                    infoBox.text = "Genereerd " + sub.constantEnergyPower.ToString() + " energie per beurt";
                }
                else if (buildingType.type == Types.buildingtypes.coolsystem)  // Set information for the coolgenerator
                {
                    buildingName.text = "Koelsysteem";
                    infoBox.text = "Koelt de planeet met 100 per beurt";
                }

            }

            // Change ui elements for every type of building
            if (buildingType.bought)
            {
                if (buildingType.turnedOn)
                {
                    buildingState.text = "Aan";
                }
                else if (!buildingType.turnedOn && !buildingType.buildingDone)
                {
                    buildingState.text = "Bouwen";
                }
                else if (!buildingType.turnedOn && buildingType.buildingDone)
                {
                    buildingState.text = "Uit";
                }

                infoPopUp.transform.GetChild(1).gameObject.SetActive(true);
                infoPopUp.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                infoPopUp.transform.GetChild(1).gameObject.SetActive(false);
                infoPopUp.transform.GetChild(2).gameObject.SetActive(false);
            }

        }
        else
        {
            currentObject = null;
            infoPopUp.SetActive(false);
        }

    }


    public void RemoveBuilding()
    {
        if (currentObject != null)
        {
            BuildingManager.I.RemoveBuilding(currentObject, gridManager, true);
            infoPopUp.SetActive(false);
            deleteButton.SetActive(false);
        }

    }

    public void OnmouseExit()
    {
        infoPopUp.SetActive(false);
    }

}
