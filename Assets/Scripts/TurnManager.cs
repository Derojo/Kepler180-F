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

    public bool checkedLevelComplete = false;
    public bool LevelCompleted = false;
    public bool levelLostNoTurns = false;
    public bool levelLostNoPower = false;

    private bool showedMessage = false;

    public int levelToUnlock = 2;
    // Use this for initialization
    void Start()
    {

        // Setting turndisplay
        maxTurns = LevelManager.I.M_T_A;
        turnsLeft = maxTurns +- 1;
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
            winLevel();

        }
        if (AuraManager.I.auraLevelPercentage >= 100 && !LevelCompleted)
        {
            LevelCompleted = true;
            EventManager.TriggerEvent("updateUI");
            //SceneManager.LoadSceneAsync("Evaluation");
        }
        if (turnsLeft == 0)
        {

            //If >= maxTurns: open Check game condition function
            levelLostNoTurns = true;
            EventManager.TriggerEvent("updateUI");
        }


        //check resources
        if (ResourceManager.I.powerLevel <= 0 )
        {
            Debug.Log("Powerlevel is low");
        
                if (checkedLevelComplete)
                {
                    checkedLevelComplete = false;
                }
                levelLostNoPower = true;
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
        Debug.Log("set placements done to zero");
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


    public void winLevel()
    {
        Debug.Log("Level WOn");
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        levelToUnlock++;
    }
}
