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

    public bool checkedLevelComplete = false;

    // Use this for initialization
    void Start()
    {
        // Setting turndisplay
        maxTurns = LevelManager.I.M_T_A;
        turnsLeft = maxTurns;
        //Setting aura percentage
        AuraManager.I.CalculateAuraPercentage();
        
    }

    // Update is called once per frame
    void Update()
    {
        //check if minimum aura % is reached
        if (AuraManager.I.auraLevelPercentage >= 55 && !checkedLevelComplete)
        {
            checkedLevelComplete = true;
            EventManager.TriggerEvent("updateUI");
            //Check Setskill function for adjusting difficulty
            LevelManager.I.SetSkillLevel();
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
    public void OnButtonEndTurn()
    {
        EventManager.TriggerEvent("EndTurn");
        EventManager.TriggerEvent("updateUI");
    }

    //Setting end turn text
    void setNextTurn()
    {
        Debug.Log("aurapercentage"+(AuraManager.I.auraLevelPercentage));
        //Adding turns
        turnCount++;
        //currentTurn.text = turnCount + " / " + maxTurns.ToString();
        turnsLeft--;

        //Check if max amout of turns is reached
        if (turnsLeft == 0)
        {
            //If >= maxTurns: open Check game condition function
            SceneManager.LoadSceneAsync("Evaluation");
        }
    
        //check fundings
        if(ResourceManager.I.fundings <=0 || ResourceManager.I.powerLevel <= 0 && !checkedLevelComplete)
        {
           SceneManager.LoadSceneAsync("Evaluation");
            Debug.Log("you are out of resources and lost the game, please try again");
        }
    }

}
