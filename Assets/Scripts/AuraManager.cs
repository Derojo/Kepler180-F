using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Prefab("AuraManager", true, "")]
public class AuraManager : Singleton<AuraManager>
{
    public Text auraDisplay;

    // Use this for initialization
    void Start ()
    {
        auraDisplay.text = "Aura % 0  ";
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void CalculateAuraPercentage()
    {
        
        LevelManager.I.auraLevelPercentage = (LevelManager.I.auraPower / LevelManager.I.auraPowerTotal)*100;
        auraDisplay.text = "Aura %  " + LevelManager.I.auraLevelPercentage.ToString();

    }
}
