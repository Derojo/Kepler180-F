using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Prefab ("TurnManager", true,"")]
public class TurnManager : Singleton<TurnManager>{

    public int turnCount=0;
    public Text turnCountText;
    public float maxTurns;
    public Text maxTurnDisplay;

    public bool checkedLevelComplete = false;

    // Use this for initialization
    void Start ()
    {
       // Setting turndisplay
       turnCountText.text = "Turns: ";
        maxTurns = LevelManager.I.turnMax;
        maxTurnDisplay.text = "Turns left: " + maxTurns.ToString();
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
       
        //LevelManager.I.auraPower ++;
        //Adding turns
        turnCount++;
        turnCountText.text =  "Turn: " + turnCount.ToString();
        maxTurns--;
        maxTurnDisplay.text = "Turns left: " + maxTurns.ToString();

        //Calculate aurapercentage
        AuraManager.I.CalculateAuraPercentage();
        Debug.Log(AuraManager.I.currentAuraPower + "aurapower");

        //Check if max amout of turns is reached
        if (maxTurns == 0)
        {
            //If >= maxTurns: open Check game condition function
            Debug.Log("no turns left");
        }

        //check if minimum aura % is reached
        if(AuraManager.I.auraLevelPercentage >= 55 && !checkedLevelComplete )
        {
            Debug.Log("Completed level");
            Debug.Log(turnCount);
            checkedLevelComplete = true;
            //Check Setskill function for adjusting difficulty
            LevelManager.I.SetSkillLevel();
        }
    }

}
