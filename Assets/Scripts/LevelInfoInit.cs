using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfoInit : MonoBehaviour {

 
    Level currentLevel;

    // Use this for initialization
    void Start ()
    {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void firstLevel()
    {
        LevelManager.I.M_T_A = 20;
        LevelManager.I.turnMaxText.text = LevelManager.I.M_T_A.ToString();
        AuraManager.I.auraPercentage = 55;
        AuraManager.I.auraPercText.text = AuraManager.I.auraPercentage.ToString() + " %";
        AuraManager.I.AuraColor.color = new Color32(129, 0, 115, 255);

        //Settting goals
        LevelManager.I.A_P_T = 10;
}



}
