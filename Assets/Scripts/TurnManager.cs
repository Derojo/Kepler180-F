using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Prefab ("TurnManager", true,"")]
public class TurnManager : Singleton<TurnManager>{

    public int turnCount = 1;
    public float maxTurns;
    public float turnsLeft;
    public Text currentTurn;
    public Text turnsleft;

    public bool checkedLevelComplete = false;

    // Use this for initialization
    void Start()
    {
        // Setting turndisplay
        maxTurns = LevelManager.I.M_T_A;
        turnsLeft = maxTurns;
        turnsleft.text = "nog " + maxTurns.ToString() + " beurten over";
        currentTurn.text = turnCount + " / " + maxTurns.ToString();
        //Setting aura percentage
        AuraManager.I.CalculateAuraPercentage();
    }

    // Update is called once per frame
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
    }

    //Setting end turn text
    void setNextTurn()
    {

        //Adding turns
        turnCount++;
        currentTurn.text = turnCount + " / " + maxTurns.ToString();
        turnsLeft--;
        turnsleft.text = "nog " + turnsLeft.ToString() + " beurten over";

        //Check if max amout of turns is reached
        if (maxTurns == 0)
        {
            //If >= maxTurns: open Check game condition function
            Debug.Log("no turns left");
        }

        //check if minimum aura % is reached
        if (AuraManager.I.auraLevelPercentage >= 55 && !checkedLevelComplete )
        {
            Debug.Log("Completed level");
            Debug.Log(turnCount);
            checkedLevelComplete = true;
            //Check Setskill function for adjusting difficulty
            LevelManager.I.SetSkillLevel();
        }
        //check fundings
        if(ResourceManager.I.fundings <=0 || ResourceManager.I.powerLevel <= 0 && !checkedLevelComplete)
        {
            Debug.Log("you are out of resources and lost the game, please try again");
        }
    }

}
