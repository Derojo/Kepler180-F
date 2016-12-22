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

    //public int buildTime = 2;
    public int sizeOnGrid = 1;
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
        if (!blueprint)
        {
            buildingTurnInfo = buildingInfoCanvas.transform.GetChild(0).gameObject;
            buildingKrachtInfo = buildingInfoCanvas.transform.GetChild(3).gameObject;
            buildTimeInfo = buildingTurnInfo.transform.GetChild(1).GetComponent<Text>();
            buildTimeInfo.text = gameObject.GetComponent < BuildingType>().buildTime.ToString();

            buildingKrachtInfo.transform.GetChild(1).GetComponent<Text>().text = auraPower.ToString() + " kracht";
        }
        else {
            this.GetComponent<Renderer>().material.renderQueue = 5000;
        }

    }

    //StartListening 
    void OnEnable()
    {
        if(!blueprint)
        {
            EventManager.StartListening("EndTurn", setNextTurn);
        }
        EventManager.StartListening("BuildingCompleted", buildingCompleted);
    }
    //unregistering listeners for clean up
    void OnDisable()
    {
        EventManager.StartListening("BuildingCompleted", buildingCompleted);

        if (!blueprint)
        {
            EventManager.StopListening("EndTurn", setNextTurn);
        }
    }

    void setNextTurn()
    {
        if (turnedOn)
        {    
            AuraManager.I.currentAuraPower = AuraManager.I.currentAuraPower + auraPower;
            AuraManager.I.CalculateAuraPercentage();

        Debug.Log(AuraManager.I.currentAuraPower);
        }
    }

    void buildingCompleted()
    {
        Debug.Log("buidlingcomplete called");
        if (!turnedOn && gameObject.GetComponent<BuildingType>().buildTime ==0)
        {
            buildingInfoCanvas.transform.GetChild(1).gameObject.SetActive(false);
            buildingInfoCanvas.transform.GetChild(2).gameObject.SetActive(true);
            turnedOn = true;

            buildingKrachtInfo.SetActive(true);
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            return;
        }
    }
}
