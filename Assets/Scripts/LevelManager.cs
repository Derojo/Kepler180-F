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

    // Level states
    public bool checkedLevelComplete = false;
    public bool levelCompleted = false;
    public bool levelLostNoTurns = false;
    public bool levelLostNoPower = false;

    public void Load() { return; }
    // Use this for initialization
    void Start()
    {
        levelData = LevelContainer.Load(path);
        SetCurrentLevel();
    }

    public void setTutorialLevel() {

        M_T_A = 20;
        A_P_T = 2500;
        ResourceManager.I.setTutorialValues();
        AuraManager.I.A_C = "Paars";
        AuraManager.I.A_C_C = "199, 0, 255, 255";
        AuraManager.I.colorTypeCode = 1;
        AuraManager.I.isBlend = true;
        TurnManager.I.maxTurns = 20;
        TurnManager.I.turnsLeft = 20;
    }

    public void SetCurrentLevel()
    {

        AuraManager.I.A_C = levelData.levels[(currentLevel - 1)].A_C;
        AuraManager.I.colorTypeCode = levelData.levels[(currentLevel - 1)].colorTypeCode;
        AuraManager.I.isBlend = levelData.levels[(currentLevel - 1)].isBlend;
        AuraManager.I.A_C_C = levelData.levels[(currentLevel - 1)].A_C_C;
        if (AuraManager.I.isBlend)
        {
            AuraManager.I.checkBlendingColors();
        }
        ResourceManager.I.powerLevel = levelData.levels[(currentLevel - 1)].StartupPower;
        ResourceManager.I.fundings = levelData.levels[(currentLevel - 1)].StartupMoney;
        ResourceManager.I.planetHeat = levelData.levels[(currentLevel - 1)].StartupHeat;



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

        TurnManager.I.setTurnInfo();
    }

    //function for setting player difficulty
    public void SetSkillLevel()
    {
        if(currentLevel > 2)
        {
            if (TurnManager.I.turnCount >= (AuraManager.I.percentageAverage + (AuraManager.I.percentageAverage / 100) * 50))
            {

                //Set difficulty to easy
                Debug.Log("easy");
                nextLevelSkill = 1;
            }

            if (TurnManager.I.turnCount <= (AuraManager.I.percentageAverage + (AuraManager.I.percentageAverage / 100) * 20) && (TurnManager.I.turnCount >= (AuraManager.I.percentageAverage - (AuraManager.I.percentageAverage / 100) * 50)))
            {

                //Set difficulty to medium
                Debug.Log("medium");
                nextLevelSkill = 2;
            }

            if (TurnManager.I.turnCount <= (AuraManager.I.percentageAverage - (AuraManager.I.percentageAverage / 100) * 20))
            {
                //Set difficulty to hard
                Debug.Log("hard");
                nextLevelSkill = 3;
            }

        } else
        {
            nextLevelSkill = 1;
        }
    }

    public void winLevel()
    {
        PlayerPrefs.SetInt("levelReached", currentLevel+1);
    }

    public void ResettingValues()
    {
        //resetting placement
        if(PlacementData.I.placementNodes != null && PlacementData.I.placementNodes.Count > 0)
        {
            PlacementData.I.placementNodes = null;
        }
        if (PlacementData.I.planningNodes != null && PlacementData.I.planningNodes.Count > 0)
        {
            PlacementData.I.planningNodes = null;
        }

        levelCompleted = false;
        levelLostNoTurns = false;
        levelLostNoPower = false;
        checkedLevelComplete = false;
        BlueprintManager.I.bluePrintTurnsTotal = 0;


        //resetting aura power
        AuraManager.I.currentAuraPower = 0;
        AuraManager.I.auraLevelPercentage = 0;

        AuraManager.I.resetColorAuraAmounts();
        ColorManager.I.resetClusters();

        //resetting power
        ResourceManager.I.powerPercentage = 100;
    }
}
