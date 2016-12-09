using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Prefab("LevelManager", true, "")]
public class LevelManager : Singleton<LevelManager>
{
    public int turnMax = 0;
    public Text turnMaxText;
    public int auraPercentage = 0;
    public Text auraPercText;
    public Image AuraColor;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }



    
    //End update
}//end Singleton
