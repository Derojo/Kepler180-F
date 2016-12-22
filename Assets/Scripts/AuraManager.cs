using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Prefab("AuraManager", true, "")]
public class AuraManager : Singleton<AuraManager>
{

    //aura start scherm
    public int auraPercentage;

    public float currentAuraPower;
    public float auraLevelPercentage;
    public float percentageAverage;

    public string A_C;
    public string A_C_C;

   

    // Use this for initialization
    void Start ()
    {
        
        percentageAverage = (Mathf.Round(LevelManager.I.M_T_A / 100 * 55));
        Debug.Log(percentageAverage + " Aurapercentage");
    }

    public void CalculateAuraPercentage()
    {
        auraLevelPercentage = (currentAuraPower / LevelManager.I.A_P_T) * 100;
    }
}
