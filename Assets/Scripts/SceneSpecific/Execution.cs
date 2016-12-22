using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Execution : MonoBehaviour
{

    public Text totalFunding;
    // Use this for initialization
    void Start()
    {
        totalFunding.text = "$ " + ResourceManager.I.fundings.ToString();
        BlueprintManager.I.InitializeBlueprintModels();

    }

    //eventlistner
    void OnEnable()
    {
        EventManager.StartListening("EndTurn", setNextTurn);
    }
    //unregistering listeners for clean up
    void OnDisable()
    {
        EventManager.StopListening("EndTurn", setNextTurn);
    }

    void setNextTurn()
    {
        totalFunding.text = "$ " + ResourceManager.I.fundings.ToString();
    }
}
