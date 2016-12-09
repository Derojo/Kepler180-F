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
        //Setting turndisplay
        turnCountText.text = "Turns: ";
        maxTurns = LevelManager.I.turnMax;
        maxTurnDisplay.text = "Turns left: " + maxTurns.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
      
		
	}

    //Setting end turn text
    public void setNextTurn()
    {
        turnCount++;
        turnCountText.text = "Turns left: " + "Turn: " + turnCount.ToString();
        maxTurns--;
        maxTurnDisplay.text = maxTurns.ToString();
   

        //Check if max amout of turns is reached
        if (turnCount <= maxTurns)
        {
            //If >= maxTurns: open Check game condition function
            Debug.Log("no turns left");

        }
    }


}
