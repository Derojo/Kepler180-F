﻿using System.Collections;
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
    public Text PopUpText;
    //public Text turnMaxText;

    public GameObject completeLevelButton;
    public GameObject completeLevelPopUp;
    public GameObject PopUps;
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
        completeLevelButton.SetActive(false);
        completeLevelPopUp.SetActive(false);
        PopUps.SetActive(false);
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
