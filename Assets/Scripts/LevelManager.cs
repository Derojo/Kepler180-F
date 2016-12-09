using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Prefab("LevelManager", true, "")]
public class LevelManager : Singleton<LevelManager>
{
    public int turnMax;
    public Text turnMaxText;
    public int auraPercentage = 0;
    public Text auraPercText;
    public Image AuraColor;

    // Use this for initialization
    void Start()
    {
        turnMax = 20;
}

    // Update is called once per frame
    void Update()
    {

    }



    
    //End update
}//end Singleton
