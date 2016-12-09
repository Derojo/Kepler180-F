using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Prefab ("TurnManager", true,"")]
public class TurnManager : Singleton<TurnManager>{

    public int turnCount=0;
    public Text turnCountText;
    public int maxTurns;
    public Text maxTurnDisplay;

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
	void Update ()
    {
      
		
	}

    //Setting end turn text
    public void setNextTurn()
    {
        LevelManager.I.auraPower ++;
        //Adding turns
        turnCount++;
        turnCountText.text =  "Turn: " + turnCount.ToString();
        maxTurns--;
        maxTurnDisplay.text = "Turns left: " + maxTurns.ToString();
        //Calculate aurapercentage
        AuraManager.I.CalculateAuraPercentage();

        //Check if max amout of turns is reached
        if (maxTurns == 0)
        {
            //If >= maxTurns: open Check game condition function
            Debug.Log("no turns left");
        }
    }


}
