using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Prefab ("TurnManager", true,"")]
public class TurnManager : Singleton<TurnManager>{

    public int turnCount=0;
    public Text turnCountText;
    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //Setiing end turn text
    public void setNextTurn()
    {
        turnCount++;
        turnCountText.text = "Turn: " + turnCount.ToString();
        Debug.Log(turnCount);
    }
}
