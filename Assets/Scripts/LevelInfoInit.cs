using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfoInit : MonoBehaviour {

    public const string path = "levels";
    Level currentLevel;

    // Use this for initialization
    void Start ()
    {
        LevelContainer ic = LevelContainer.Load(path);
        foreach (Level level in ic.levels)
        {
            print(level.name);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void firstLevel()
    {
        LevelManager.I.turnMax = 20;
        LevelManager.I.turnMaxText.text = LevelManager.I.turnMax.ToString();
        AuraManager.I.auraPercentage = 55;
        AuraManager.I.auraPercText.text = AuraManager.I.auraPercentage.ToString() + " %";
        AuraManager.I.AuraColor.color = new Color32(129, 0, 115, 255);

        //Settting goals
        LevelManager.I.auraPowerTotal = 10;
}



}
