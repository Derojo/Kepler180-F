using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorGenerator : MonoBehaviour
{
    public Types.colortypes selectedColor;
    public int ColorGeneratorCode;
    public string ColorGeneratorName;

    public bool blueprint = false;
    public float buildingAuraPower;
    public int sizeOnGrid = 1;
    public Canvas buildingInfoCanvas;
    private GameObject buildingTurnInfo;
    private GameObject buildingKrachtInfo;
    private Text buildTimeInfo;

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
            buildTimeInfo.text = gameObject.GetComponent<BuildingType>().buildTime.ToString();

            buildingKrachtInfo.transform.GetChild(1).GetComponent<Text>().text = buildingAuraPower.ToString() + " kracht";
        }
        else
        {
            this.GetComponent<Renderer>().material.renderQueue = 5000;
        }

    }


    public void turnOnGenerator()
    {
        buildingInfoCanvas.transform.GetChild(1).gameObject.SetActive(false);
        buildingInfoCanvas.transform.GetChild(2).gameObject.SetActive(true);
        buildingKrachtInfo.SetActive(true);
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        return;
    }


    public void addAuraPower()
    {
        AuraManager.I.currentAuraPower = AuraManager.I.currentAuraPower + buildingAuraPower;
        AuraManager.I.CalculateAuraPercentage();
    }
}
