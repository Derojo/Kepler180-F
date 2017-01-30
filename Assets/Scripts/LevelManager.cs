﻿using System.Collections;
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

    public void Load() { return; }
    // Use this for initialization
    void Start()
    {
       
        levelData = LevelContainer.Load(path);
        SetCurrentLevel();
    }
    
    public void SetCurrentLevel()
    {

        Debug.Log(currentLevel - 1 + "xml");
     

        AuraManager.I.A_C = levelData.levels[(currentLevel - 1)].A_C;
        AuraManager.I.colorTypeCode = levelData.levels[(currentLevel - 1)].colorTypeCode;
        AuraManager.I.isBlend = levelData.levels[(currentLevel - 1)].isBlend;
        AuraManager.I.A_C_C = levelData.levels[(currentLevel - 1)].A_C_C;

        ResourceManager.I.powerLevel = levelData.levels[(currentLevel - 1)].StartupPower;
        ResourceManager.I.fundings = levelData.levels[(currentLevel - 1)].StartupMoney;
        ResourceManager.I.planetHeat = levelData.levels[(currentLevel - 1)].StartupHeat;

        Debug.Log(levelData.levels[(currentLevel - 1)].StartupPower);
        Debug.Log(ResourceManager.I.powerLevel + "Power in resources");
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
        
        if (TurnManager.I.turnCount >= (AuraManager.I.percentageAverage + (AuraManager.I.percentageAverage /100)*50))
        {
            
            //Set difficulty to easy
            Debug.Log("easy");
            nextLevelSkill = 1;
        }
  
        if(TurnManager.I.turnCount <= (AuraManager.I.percentageAverage + (AuraManager.I.percentageAverage / 100) * 20) && (TurnManager.I.turnCount >= (AuraManager.I.percentageAverage - (AuraManager.I.percentageAverage / 100) * 50)))
        {
            
            //Set difficulty to medium
            Debug.Log("medium");
            nextLevelSkill = 2;
        }

        if (TurnManager.I.turnCount <= (AuraManager.I.percentageAverage -(AuraManager.I.percentageAverage / 100) * 20))
        {
            //Set difficulty to hard
            Debug.Log("hard");
            nextLevelSkill = 3;
        }

    }
    public void ResettingValues()
    {
        //resetting placement
        PlacementData.I.placementNodes = null;
        PlacementData.I.planningNodes = null;
        LevelManager.I.SetCurrentLevel();

        //resetting turns
        TurnManager.I.maxTurns = LevelManager.I.M_T_A;
        TurnManager.I.turnsLeft = TurnManager.I.maxTurns;
        TurnManager.I.turnCount = 0;
        TurnManager.I.levelLostNoTurns = false;
        BlueprintManager.I.bluePrintTurnsTotal = 0;


        //resetting aura power
        AuraManager.I.currentAuraPower = 0;
        AuraManager.I.auraLevelPercentage = 0;
        AuraManager.I.auraPercentage = 0;
        LevelManager.I.A_P_T = LevelManager.I.A_P_T;
        AuraManager.I.resetColorAuraAmounts();

        //resetting power
        ResourceManager.I.powerPercentage = 100;

        TurnManager.I.levelLostNoPower = false;
        TurnManager.I.checkedLevelComplete = false;

        Debug.Log("resetting values");

    }
}
