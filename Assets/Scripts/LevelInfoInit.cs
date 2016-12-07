using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfoInit : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        firstLevel();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void firstLevel()
    {
        LevelManager.I.turnMax = 20;
        LevelManager.I.turnMaxText.text = LevelManager.I.turnMax.ToString();
        LevelManager.I.auraPercentage = 55;
        LevelManager.I.auraPercText.text = LevelManager.I.auraPercentage.ToString() + " %";
    }



}
