using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Execution : MonoBehaviour
{
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

    public Text redAmount;
    public Text blueAmount;
    public Text greenAmount;
    public Text yellowAmount;
    //infopanel 
    public Text buildingName;
    public Text buildingTime;
    public Text powerInput;
    public Text buildingCost;
    public Image AuraColorimg;
    //blueprint UI
    public Text planningMoney;
    public Text planningTurns; 

    // Use this for initialization
    void Start()
    {
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

        //loading/showing aura color Goals
        string[] rgba = AuraManager.I.A_C_C.Split(new string[] { "," }, StringSplitOptions.None);
        byte r = byte.Parse(rgba[0]);
        byte g = byte.Parse(rgba[1]);
        byte b = byte.Parse(rgba[2]);
        byte a = byte.Parse(rgba[3]);
        AuraColorimg.color = new Color32(r, g, b, a);
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
                    BuildingType infoB = hitObject.transform.GetComponent<BuildingType>();
                    infoPopUp.SetActive(true);
                    if(infoPopUp)
                    {
                        if (hitObject.transform.GetComponent<ColorGenerator>())
                        {
                            ColorGenerator generator = hitObject.transform.GetComponent<ColorGenerator>();
                            // Kleurgenerator, zet alle informatie
                            if (generator.selectedColor == Types.colortypes.Red)
                            {
                                buildingName.text = "Rode Generator";
                                buildingTime.text = infoB.buildTimeTotal.ToString();
                                powerInput.text = "- " + infoB.buildingPowerUsage.ToString();
                                buildingCost.text = "- " + infoB.buildingCost.ToString();

                                Debug.Log("Ik ben rood");
                            }
                            else if (generator.selectedColor == Types.colortypes.Blue)
                            {
                                buildingName.text = "Blauwe Generator";
                                buildingTime.text = infoB.buildTimeTotal.ToString();
                                powerInput.text = "- " + infoB.buildingPowerUsage.ToString();
                                buildingCost.text = "- " + infoB.buildingCost.ToString();
                                Debug.Log("Ik ben blauw");
                            }
                            //green
                            else if (generator.selectedColor == Types.colortypes.Green)
                            {
                                buildingName.text = "Groene Generator";
                                buildingTime.text = infoB.buildTimeTotal.ToString();
                                powerInput.text = "- " + infoB.buildingPowerUsage.ToString();
                                buildingCost.text = "- " + infoB.buildingCost.ToString();
                                Debug.Log("Ik ben groen");
                            }
                            //yellow
                            else if (generator.selectedColor == Types.colortypes.Yellow)
                            {
                                buildingName.text = "Gele Generator";
                                buildingTime.text = infoB.buildTimeTotal.ToString();
                                powerInput.text = "- " + infoB.buildingPowerUsage.ToString();
                                buildingCost.text = "- " + infoB.buildingCost.ToString();
                                Debug.Log("Ik ben geel");
                            }
                            // etc etc
                        }
                    }
                   
                }
                else
                {
                    infoPopUp.SetActive(false);
                }
            
            }

        }
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

        if (TurnManager.I.levelLostNoResources)
        {
            PopUpText.text = "Helaas je hebt geen resources meer om 55% aurakracht te behalen";
            PopUps.SetActive(true);
        }
    }

    void UpdatePlanningUI()
    {
        planningMoney.text = BlueprintManager.I.bluePrintMoneyTotal.ToString();
        planningTurns.text = BlueprintManager.I.bluePrintTurnsTotal.ToString() + "/" + LevelManager.I.M_T_A.ToString();
        Debug.Log(BlueprintManager.I.bluePrintTurnsTotal);
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
}
