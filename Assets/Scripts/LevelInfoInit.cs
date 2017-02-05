using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelInfoInit : MonoBehaviour {

    public Text auraPercText;
    public Image AuraColor;
    public Text turnsInit;
    public Text currentLevel;

    // Use this for initialization
    void Start ()
    {
        Manager.I.Load();
        InitGoals();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void InitGoals()
    {
        //setting minimum aurapercentage goal
        currentLevel.text = "Level " + LevelManager.I.currentLevel;
        auraPercText.text = AuraManager.I.auraPercentage.ToString() + " %";
        turnsInit.text = LevelManager.I.M_T_A.ToString();
        //setting auracolor goal
        string[] rgba = AuraManager.I.A_C_C.Split(new string[] { "," }, StringSplitOptions.None);
        byte r = byte.Parse(rgba[0]);
        byte g = byte.Parse(rgba[1]);
        byte b = byte.Parse(rgba[2]);
        byte a = byte.Parse(rgba[3]);
        AuraColor.color = new Color32(r, g, b, a);
    }

    public void LevelInitButtons()
    {
        AudioManager.I.source[6].Play();
    }

}
