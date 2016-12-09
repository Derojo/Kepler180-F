using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Prefab("LevelManager", true, "")]
public class LevelManager : Singleton<LevelManager>
{
    public int turnMax;
    public Text turnMaxText;

    //aura start
    public int auraPercentage;
    public Text auraPercText;
    public Image AuraColor;

    //aura level
    public float auraPower = 0;
    public float auraPowerTotal = 10;
    public float auraLevelPercentage;

    // Use this for initialization
    void Start()
    {
        
}

    // Update is called once per frame
    void Update()
    {

    }

   
    
    //End update
}//end Singleton
