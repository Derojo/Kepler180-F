using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Prefab("LevelManager", true, "")]
public class LevelManager : Singleton<LevelManager>
{

   

    public float turnMax;
    public Text turnMaxText;

    //aura start
    public int auraPercentage;
    public Text auraPercText;
    public Image AuraColor;

    //aura level
    public float auraPower = 0;
    public float auraPowerTotal = 20;
    public float auraLevelPercentage;
    public float percentageAverage;
    //level difficulty
    public int nextLevelSkill;

    // Use this for initialization
    void Start()
    {
       
        percentageAverage = (Mathf.Round(turnMax / 100 * 55));
        Debug.Log(percentageAverage + " Aurapercentage");

       foreach(Level level in ic.levels)
        {
            print(level.name);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    //function for setting player difficulty
    public void SetSkillLevel()
    {
        
      
        if (TurnManager.I.turnCount >= (percentageAverage + (percentageAverage/100)*30))
        {
            //Set difficulty to easy
            Debug.Log("easy");
            nextLevelSkill = 1;
        }
  
        if(TurnManager.I.turnCount <= (percentageAverage + (percentageAverage / 100) * 30) && (TurnManager.I.turnCount >= (percentageAverage - (percentageAverage / 100) * 30)))
        {
            //Set difficulty to medium
            Debug.Log("medium");
            nextLevelSkill = 2;
        }

        if (TurnManager.I.turnCount <= (percentageAverage -(percentageAverage / 100) * 30))
        {
            //Set difficulty to hard
            Debug.Log("hard");
            nextLevelSkill = 3;
        }

    }


    //End update
}//end Singleton
