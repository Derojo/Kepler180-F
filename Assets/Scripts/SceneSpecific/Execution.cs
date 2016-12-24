using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Execution : MonoBehaviour
{

    public Text totalFunding;
    public Text auraDisplay;
    public Text totalPower;
    // Use this for initialization
    void Start()
    {
        BlueprintManager.I.InitializeBlueprintModels();

    }

    //eventlistner
    void OnEnable()
    {
        EventManager.StartListening("updateUI", UpdateUI);
    }
    //unregistering listeners for clean up
    void OnDisable()
    {
        EventManager.StopListening("updateUI", UpdateUI);
    }


    void UpdateUI()
    {
        //Updating Aura UI
        auraDisplay.text = "Aura %  " + AuraManager.I.auraLevelPercentage.ToString();
        //Updating funding UI
        totalFunding.text = "$ " + ResourceManager.I.fundings.ToString();
        //Updating power UI
        totalPower.text = "power " + ResourceManager.I.powerLevel.ToString();

    }
}
