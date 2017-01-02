using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Execution : MonoBehaviour
{

    public Text totalFunding;
    public Text auraDisplay;
    public Text totalPower;

    public Text turnsleft;
    public Text currentTurn;

    public Text powerAuraText;
    //public Text turnMaxText;

    public GameObject completeLevel;
    // Use this for initialization
    void Start()
    {
        BlueprintManager.I.InitializeBlueprintModels();
        UpdateUI();
        turnsleft.text = "nog " + TurnManager.I.maxTurns.ToString() + " beurten over";
        currentTurn.text = TurnManager.I.turnCount + " / " + TurnManager.I.maxTurns.ToString();

        //turnMaxText.text = LevelManager.I.M_T_A.ToString();
        powerAuraText.text = LevelManager.I.A_P_T.ToString();

        //set complete level button to false
        completeLevel.SetActive(false);
    }

    //eventlistner
    void OnEnable()
    {
        EventManager.StartListening("updateUI", UpdateUI);
    }
    //unregistering listeners for clean up
    void OnDisable()
    {
        EventManager.StopListening("updateUI", UpdateUI);
    }


    void UpdateUI()
    {
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
            completeLevel.SetActive(true);
         }
    }

    public void QuitLevel()
    {
        SceneManager.LoadSceneAsync("Evaluation");
    }
}
