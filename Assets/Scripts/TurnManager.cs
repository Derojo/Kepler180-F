using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Prefab ("TurnManager", true,"")]
public class TurnManager : Singleton<TurnManager>{

    public int turnCount = 1;
    public float maxTurns;
    public float turnsLeft;
    public int maxPlacementsPerTurn;
    public int placementsDone = 0;


    private bool showedMessage = false;

    public void Load() { return; }
    // Use this for initialization
    public void setTurnInfo()
    {
        // Setting turndisplay
        maxTurns = LevelManager.I.M_T_A;
        turnsLeft = maxTurns +- 1;
        turnCount = 0;

        //Setting aura percentage
        AuraManager.I.CalculateAuraPercentage();
    }

    // Update is called once per frame
    void Update()
    {
        //check resources
        if (ResourceManager.I.powerLevel <= 0 && !LevelManager.I.levelLostNoPower)
        {
            if (LevelManager.I.checkedLevelComplete)
            {
                LevelManager.I.checkedLevelComplete = false;
            }
            Debug.Log("levellostnopower to true");
            LevelManager.I.levelLostNoPower = true;
            EventManager.TriggerEvent("updateUI");
        }
        //check if minimum aura % is reached
        if (AuraManager.I.auraLevelPercentage >= 55 && !LevelManager.I.checkedLevelComplete && !LevelManager.I.levelLostNoPower)
        {
            LevelManager.I.checkedLevelComplete = true;
            EventManager.TriggerEvent("updateUI");
            //Check Setskill function for adjusting difficulty
            LevelManager.I.SetSkillLevel();
            if(SceneManager.GetActiveScene().name  != "Tutorial")
            {
                LevelManager.I.winLevel();
            }

        }
        if (AuraManager.I.auraLevelPercentage >= 100 && !LevelManager.I.levelCompleted && !LevelManager.I.levelLostNoPower)
        {
            Debug.Log(LevelManager.I.levelLostNoPower);
            LevelManager.I.levelCompleted = true;
            EventManager.TriggerEvent("updateUI");
            //SceneManager.LoadSceneAsync("Evaluation");
        }
        if (turnsLeft == 0)
        {

            //If >= maxTurns: open Check game condition function
            LevelManager.I.levelLostNoTurns = true;
            EventManager.TriggerEvent("updateUI");
        }



    }
  
    //StartListening 
    void OnEnable()
    {
        EventManager.StartListening("EndTurn", setNextTurn);
    }

    //unregistering listeners for clean up
    void OnDisable()
    {
        EventManager.StopListening("EndTurn", setNextTurn);
    }

    // end turn eand call EndTurn event
    public void EndTurn()
    {
        placementsDone = 0;
        EventManager.TriggerEvent("EndTurn");
        EventManager.TriggerEvent("updateUI");
    }

    //Setting end turn text
    void setNextTurn()
    {
        
        //Adding turns
        turnCount++;
        //currentTurn.text = turnCount + " / " + maxTurns.ToString();
        turnsLeft--;
        //Check if max amout of turns is reached
    }

}
