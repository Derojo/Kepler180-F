using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorGenerator : MonoBehaviour
{
    public Types.colortypes selectedColor;
    public int ColorGeneratorCode;
    public string ColorGeneratorName;
    public float auraPower;
    public bool blueprint = false;

    public int buildTime = 2;
    public int sizeOnGrid = 1;
    public bool buildingDone = false;
    public bool turnedOn = false;
    public Canvas buildingInfoCanvas;
    private GameObject buildingTurnInfo;
    private GameObject buildingKrachtInfo;
    private Text buildTimeInfo;
    public bool generatePower = false;
    // Use this for initialization
    void Start()
    {
        ColorGeneratorCode = (int)selectedColor;
        ColorGeneratorName = selectedColor.ToString();
        if (!blueprint) {
            buildingTurnInfo = buildingInfoCanvas.transform.GetChild(0).gameObject;
            buildingKrachtInfo = buildingInfoCanvas.transform.GetChild(3).gameObject;
            buildTimeInfo = buildingTurnInfo.transform.GetChild(1).GetComponent<Text>();
            buildTimeInfo.text = buildTime.ToString();

            buildingKrachtInfo.transform.GetChild(1).GetComponent<Text>().text = auraPower.ToString() + " kracht";
        }

    }

    //StartListening 
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
        Debug.Log(buildTime + "buildtime");
        Debug.Log(buildingDone);

        if (!buildingDone)
        {
            buildTime--;
            buildTimeInfo.text = buildTime.ToString();
        }
        
        if (buildTime == 0)
        {
            buildingTurnInfo.SetActive(false);
            buildingDone = true;       
        }

        if (buildingDone && !turnedOn)
        {
            buildingInfoCanvas.transform.GetChild(1).gameObject.SetActive(false);
            buildingInfoCanvas.transform.GetChild(2).gameObject.SetActive(true);
            turnedOn = true;
            buildingKrachtInfo.SetActive(true);
            return;
        }
        if (buildingDone && turnedOn) {
            AuraManager.I.currentAuraPower = AuraManager.I.currentAuraPower + auraPower;
            AuraManager.I.CalculateAuraPercentage();
        }
    }
}
