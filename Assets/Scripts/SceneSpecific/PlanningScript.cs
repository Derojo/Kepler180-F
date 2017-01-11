using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlanningScript : MonoBehaviour {

    public Text totalPlanningCost;
    public Text totalTurnCostText;
    public Text currentTurnCostText;
    public Image AuraColorIMG;
    //eventlistner
    void OnEnable()
    {
        EventManager.StartListening("updateplanUI", UpdatePlanningUI);
    }
    //unregistering listeners for clean up
    void OnDisable()
    {

        EventManager.StopListening("updateplanUI", UpdatePlanningUI);
    }
    // Use this for initialization
    void Start ()
    {
        Manager.I.Load();
        //loading/diplaying turn info
        currentTurnCostText.text = BlueprintManager.I.bluePrintTurnsTotal.ToString() + "/" + LevelManager.I.M_T_A.ToString();
        //loading/showing aura color Goals
        string[] rgba = AuraManager.I.A_C_C.Split(new string[] { "," }, StringSplitOptions.None);
        byte r = byte.Parse(rgba[0]);
        byte g = byte.Parse(rgba[1]);
        byte b = byte.Parse(rgba[2]);
        byte a = byte.Parse(rgba[3]);
        AuraColorIMG.color = new Color32(r, g, b, a);

    }

    // Update is called once per frame
    void UpdatePlanningUI ()
    {
        currentTurnCostText.text = BlueprintManager.I.bluePrintTurnsTotal.ToString() + "/" + LevelManager.I.M_T_A.ToString();
        totalPlanningCost.text = BlueprintManager.I.bluePrintMoneyTotal.ToString();
    }

}
