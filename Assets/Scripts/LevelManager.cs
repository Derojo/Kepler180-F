using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Prefab("LevelManager", true, "")]
public class LevelManager : Singleton<LevelManager>
{

   
    //Defining maximum amount of turns & total aurapower need for levelcompletion
    public float turnMax;
    public Text turnMaxText;
    public float auraPowerTotal = 20;
    //aura level

    //level difficulty
    public int nextLevelSkill;

    // Use this for initialization
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    //function for setting player difficulty
    public void SetSkillLevel()
    {
        
      
        if (TurnManager.I.turnCount >= (AuraManager.I.percentageAverage + (AuraManager.I.percentageAverage /100)*30))
        {
            //Set difficulty to easy
            Debug.Log("easy");
            nextLevelSkill = 1;
        }
  
        if(TurnManager.I.turnCount <= (AuraManager.I.percentageAverage + (AuraManager.I.percentageAverage / 100) * 30) && (TurnManager.I.turnCount >= (AuraManager.I.percentageAverage - (AuraManager.I.percentageAverage / 100) * 30)))
        {
            //Set difficulty to medium
            Debug.Log("medium");
            nextLevelSkill = 2;
        }

        if (TurnManager.I.turnCount <= (AuraManager.I.percentageAverage -(AuraManager.I.percentageAverage / 100) * 30))
        {
            //Set difficulty to hard
            Debug.Log("hard");
            nextLevelSkill = 3;
        }

    }


    //End update
}//end Singleton
