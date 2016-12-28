using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelInfoInit : MonoBehaviour {

    Level currentLevel;
    public Text auraPercText;
    public Image AuraColor;
    public Text turnsInit;
    // Use this for initialization
    void Start ()
    {
        InitGoals();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void InitGoals()
    {
       //setting max turns
        //LevelManager.I.turnMaxText.text = LevelManager.I.M_T_A.ToString();
        //setting minimum aurapercentage goal
        AuraManager.I.auraPercentage = 55;
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



}
