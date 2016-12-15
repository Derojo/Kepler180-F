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
    public Text auraPercText;
    public Image AuraColor;

    public float currentAuraPower;
    public float auraLevelPercentage;
    public float percentageAverage;


    public Text auraDisplay;

    // Use this for initialization
    void Start ()
    {
        auraDisplay.text = "Aura % 0  ";
        percentageAverage = (Mathf.Round(LevelManager.I.turnMax / 100 * 55));
        Debug.Log(percentageAverage + " Aurapercentage");
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void CalculateAuraPercentage()
    {
        
        auraLevelPercentage = (currentAuraPower / LevelManager.I.auraPowerTotal)*100;
        auraDisplay.text = "Aura %  " + auraLevelPercentage.ToString();

    }
}
