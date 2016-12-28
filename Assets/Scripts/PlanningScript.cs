using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlanningScript : MonoBehaviour {

    public Text totalPlanningCost;
    public Text totalTurnCostText;
    public Text currentTurnCostText;
    

    // Use this for initialization
    void Start ()
    {

		totalTurnCostText.text = LevelManager.I.M_T_A.ToString();

    }
	
	// Update is called once per frame
	void Update ()
    {

    }
}
