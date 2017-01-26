using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EvaluationScript : MonoBehaviour
{
    public Text levelStatus;
    public Text endingAurapower;
    public Text endingTurns;
    public Text endingFunds;
    public Text endingEnergy;

    public GameObject levelWonButton;
    public GameObject levelLostButton;

    public Sprite completedLevelIMG;
    public Sprite failedLevelIMG;

    public Image ConditionIMG;

    // Use this for initialization
    void Start ()
    {

        levelWonButton.SetActive(false);
        levelLostButton.SetActive(false);

        if(AuraManager.I.auraLevelPercentage >= 55)
        {
            if(ResourceManager.I.powerLevel == 0)
            {
                ConditionIMG.sprite = failedLevelIMG;
                levelStatus.text = "Level verloren!";
                levelLostButton.SetActive(true);
            }
            else
            {
                ConditionIMG.sprite = completedLevelIMG;
                levelStatus.text = "Level gehaald!";
                levelWonButton.SetActive(true);
            }
          
        }
        if(AuraManager.I.auraLevelPercentage <= 55)
        {
            ConditionIMG.sprite = failedLevelIMG;
            levelStatus.text = "Level verloren!";
            levelLostButton.SetActive(true);
        }
        endingAurapower.text = "Total Aurapower: " + AuraManager.I.currentAuraPower.ToString();
        endingTurns.text = "Total Turns: " + TurnManager.I.turnCount.ToString();
        endingFunds.text = "Total Minerals: " + ResourceManager.I.fundings.ToString();
        endingEnergy.text ="Total Power: " + ResourceManager.I.powerLevel.ToString();    
    }
	
	// Update is called once per frame
	public void ContinueButton ()
    {
        SceneManager.LoadSceneAsync("Execution");
    }

    public void BackToMain()
    {
        SceneManager.LoadSceneAsync("StartMenu");
        PlacementData.I.placementNodes = null;
        PlacementData.I.planningNodes = null;
        ResettingValues();
         
        Debug.Log("resetting values");
    }

    public void ResettingValues()
    {
        LevelManager.I.SetCurrentLevel();

        //resetting turns
        TurnManager.I.maxTurns = LevelManager.I.M_T_A;
        TurnManager.I.turnsLeft = TurnManager.I.maxTurns;
        TurnManager.I.turnCount = 0;
        TurnManager.I.levelLostNoTurns = false;
        BlueprintManager.I.bluePrintTurnsTotal = 0;


    //resetting aura power
    AuraManager.I.currentAuraPower = 0;
        AuraManager.I.auraLevelPercentage = 0;
        AuraManager.I.auraPercentage = 0;
        LevelManager.I.A_P_T = LevelManager.I.A_P_T;

        //resetting power
        ResourceManager.I.powerPercentage = 100;

        TurnManager.I.levelLostNoPower = false;
        TurnManager.I.checkedLevelComplete = false; 
        //resetting resources 
      


        
    }
}
