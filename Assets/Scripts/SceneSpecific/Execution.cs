using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using RTS_Cam;

public class Execution : MonoBehaviour
{
    public UIManager uimanager;
    // Goals box
    public Animator goals;
    //Ingame text
    public Text totalFunding;
    public Text auraDisplay;
    public Text totalPower;
    public Text turnsleft;
    public Text currentTurn;
    public Text powerAuraText;
    public Text PopUpText;

    // GameObjects
    public GameObject completeLevelButton;
    public GameObject completeLevelPopUp;
    public GameObject PopUps;
    public GameObject infoPopUp;
    public GameObject escPopUp;

    public Text redAmount;
    public Text blueAmount;
    public Text greenAmount;
    public Text yellowAmount;
    //infopanel 
    public Text buildingName;
    public Text buildingTime;
    public Text powerInput;
    public Text buildingCost;
    public Text buildingState;
    public GameObject deleteButton;
    public GameObject buyButton;
    public GridManager gridManager;
    public Image AuraColorimg;
    private GameObject currentObject;
    //blueprint UI
    public Text planningMoney;
    public Text planningTurns;

    bool menuOpen = false;

    void Start()
    {
        StartCoroutine(hideGoals(2f));
        BlueprintManager.I.InitializeBlueprintModels();
        UpdateUI();
        UpdatePlanningUI();
        turnsleft.text = "nog " + TurnManager.I.maxTurns.ToString() + " beurten over";
        currentTurn.text = TurnManager.I.turnCount + " / " + TurnManager.I.maxTurns.ToString();

        //turnMaxText.text = LevelManager.I.M_T_A.ToString();
        powerAuraText.text = LevelManager.I.A_P_T.ToString();

        //set complete level button to false
        completeLevelButton.SetActive(false);
        completeLevelPopUp.SetActive(false);
        PopUps.SetActive(false);
        infoPopUp.SetActive(false);
        escPopUp.SetActive(false);

        //loading/showing aura color Goals
        string[] rgba = AuraManager.I.A_C_C.Split(new string[] { "," }, StringSplitOptions.None);
        byte r = byte.Parse(rgba[0]);
        byte g = byte.Parse(rgba[1]);
        byte b = byte.Parse(rgba[2]);
        byte a = byte.Parse(rgba[3]);
        AuraColorimg.color = new Color32(r, g, b, a);
    }

    private IEnumerator hideGoals(float time) {
        yield return new WaitForSeconds(time);
        uimanager.DisableBoolAnimator(goals);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !menuOpen)
        {
        
            escPopUp.SetActive(true);
            menuOpen = true;
            return;

        }
        if (Input.GetButtonDown("Cancel") && menuOpen)
        {
            escPopUp.SetActive(false);
            menuOpen = false;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitObject;

        if (Physics.Raycast(ray, out hitObject))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hitObject.collider.GetComponent<BuildingType>())
                {
                    Camera.main.GetComponent<RTS_Camera>().SetTarget(hitObject.collider.gameObject.transform);
                    infoPopUp.SetActive(true);
                    UpdateBuildingInfoUI(hitObject.collider.gameObject);

                }
            }
        }//end rayccast

      
    }
    //eventlistner
    void OnEnable()
    {
        EventManager.StartListening("updateplanUI", UpdatePlanningUI);
        EventManager.StartListening("updateUI", UpdateUI);

    }
    //unregistering listeners for clean up
    void OnDisable()
    {
        EventManager.StopListening("updateUI", UpdateUI);
        EventManager.StopListening("updateplanUI", UpdatePlanningUI);

    }

    void UpdateUI()
    {
        redAmount.text = AuraManager.I.GetColorAmount(Types.colortypes.Red).ToString();
        blueAmount.text = AuraManager.I.GetColorAmount(Types.colortypes.Blue).ToString();
        greenAmount.text = AuraManager.I.GetColorAmount(Types.colortypes.Green).ToString();
        yellowAmount.text = AuraManager.I.GetColorAmount(Types.colortypes.Yellow).ToString();
        //Updating Aura UI
        auraDisplay.text = "Aura %  " + AuraManager.I.auraLevelPercentage.ToString();
        //Updating funding UI
        totalFunding.text = ResourceManager.I.fundings.ToString();
        //Updating power UI
        totalPower.text = ResourceManager.I.powerLevel.ToString();
        //Update turns UI
        turnsleft.text = "nog " + TurnManager.I.turnsLeft.ToString() + " beurten over";
        currentTurn.text = TurnManager.I.turnCount + " / " + TurnManager.I.maxTurns.ToString();

        //Set ënd level button to enable if level is completed
         if(TurnManager.I.checkedLevelComplete)
         {
            completeLevelButton.SetActive(true);
         }
        if (TurnManager.I.LevelCompleted)
        {
           PopUpText.text = "Gefeliciteerd je hebt 100% aurapower behaald en daarmee het level behaald.";
            PopUps.SetActive(true);
        }

        // Update UI if player failed
        if(TurnManager.I.levelLostNoTurns)
        {
            PopUpText.text = "Helaas je hebt niet binnen de beurten 55% aurakracht behaald";
            PopUps.SetActive(true);
        }

        if (TurnManager.I.levelLostNoPower )
        {
            if (!TurnManager.I.checkedLevelComplete)
            {
                PopUpText.text = "Helaas je hebt geen stroom meer om 55% aurakracht te behalen";
            }
            else {
                PopUpText.text = "Helaas je hebt het level niet gehaald, al je gebouwen zijn uitgevallen omdat er geen stroom meer is! ";
            }

            PopUps.SetActive(true);
        }
        if (currentObject != null) {
            infoPopUp.SetActive(false);
        }
        
    }
    //updating the Ui in blueprintmode
    void UpdatePlanningUI()
    {
        planningMoney.text = BlueprintManager.I.bluePrintMoneyTotal.ToString();
        planningTurns.text = BlueprintManager.I.bluePrintTurnsTotal.ToString() + "/" + LevelManager.I.M_T_A.ToString();
        Debug.Log(BlueprintManager.I.bluePrintTurnsTotal);
    }

 
   public void UpdateBuildingInfoUI(GameObject building)
    {

        if (currentObject != building || currentObject == null) {
            currentObject = building;
        }
        infoPopUp.SetActive(true);
        Text infoBox = infoPopUp.transform.GetChild(8).transform.GetChild(0).GetComponent<Text>();
        if (infoPopUp)
        {

            // Every incoming building has a buildingType
            BuildingType buildingType = building.GetComponent<BuildingType>();

            // Set general information
            buildingTime.text = buildingType.buildTimeTotal.ToString();
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
                SubBuilding sub = building.GetComponent<SubBuilding>();
                // General
                if (!buildingType.bought) {
                    buyButton.SetActive(true);
                } else
                {
                    buyButton.SetActive(false);
                }
               
                if (buildingType.type == Types.buildingtypes.mineraldrill)  // Set information for the mineraldrill
                {
                    buildingName.text = "Mineraalboor";
                    infoBox.text = "Boort "+ sub.harvestPower.ToString() + " mineralen per beurt";
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
                } else if (buildingType.type == Types.buildingtypes.coolsystem)  // Set information for the coolgenerator
                {
                    buildingName.text = "Koelsysteem";
                    infoBox.text = "Koelt de planeet met 100 per beurt";
                }

            }

            // Change ui elements for every type of building
            if (buildingType.bought)
            {
                buyButton.SetActive(false);
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
            } else {
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

    public void RemoveBuilding() {

        if (currentObject != null) {
            BuildingManager.I.RemoveBuilding(currentObject, gridManager);
            infoPopUp.SetActive(false);
            deleteButton.SetActive(false);
        }

    }
    public void BuyBuilding()
    {
        if (currentObject != null)
        {
            if (!BuildingManager.I.AbleToBuy(currentObject.GetComponent<BuildingType>()))
            {
                uimanager.ShowMessage(Types.messages.noFunding);
            }
            else {
                BuildingManager.I.BuyBuilding(currentObject.GetComponent<BuildingType>());
                infoPopUp.SetActive(false);
                deleteButton.SetActive(false);
            }
        }
    }

    public void OnmouseExit()
    {
        infoPopUp.SetActive(false);
    }

    //buttons
    public void QuitLevelPopUp()
    {
        completeLevelPopUp.SetActive(true);
    }
    public void QuitLevel()
    {
        SceneManager.LoadSceneAsync("Evaluation");
    }
    
    public void KeepPlaying()
    {
        completeLevelPopUp.SetActive(false);
    }

    public void continuePlaying()
    {
 
        if (menuOpen)
        {
       
            escPopUp.SetActive(false);
            menuOpen = false;
        }
    }
    public void QuitLevelToMenu()
    {
        Debug.Log("clicked button levae");
        SceneManager.LoadSceneAsync("StartMenu");
    }


}
