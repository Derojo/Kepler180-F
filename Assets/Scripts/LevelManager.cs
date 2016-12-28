using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Prefab("LevelManager", true, "")]
public class LevelManager : Singleton<LevelManager>
{
    public const string path = "levels";

    //Defining maximum amount of turns & total aurapower need for levelcompletion
    //maximum turn amount
    public float M_T_A;
   
    // Total aura power
    public float A_P_T;
   

    //level difficulty
    public int nextLevelSkill;
    LevelContainer levelData;
    public int currentLevel;

    // Use this for initialization
    void Start()
    {
        levelData = LevelContainer.Load(path);
        SetCurrentLevel();

    }
    
    public void SetCurrentLevel()
    {
        AuraManager.I.A_C = levelData.levels[(currentLevel - 1)].A_C;
        AuraManager.I.A_C_C = levelData.levels[(currentLevel - 1)].A_C_C;

        Debug.Log(nextLevelSkill);
        if (nextLevelSkill == 1)
        {
            
            M_T_A = levelData.levels[(currentLevel - 1)].M_T_E;
            A_P_T = levelData.levels[(currentLevel - 1)].A_P_E;
        }
        if (nextLevelSkill == 2)
        {

            M_T_A = levelData.levels[(currentLevel - 1)].M_T_M;
            A_P_T = levelData.levels[(currentLevel - 1)].A_P_M;
        }
        if (nextLevelSkill == 3)
        {

            M_T_A = levelData.levels[(currentLevel - 1)].M_T_H;
            A_P_T = levelData.levels[(currentLevel - 1)].A_P_H;
        }

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
