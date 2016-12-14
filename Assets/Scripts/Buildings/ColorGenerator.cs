using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator : MonoBehaviour
{
    public ColorTypes.types selectedColor;
    public int ColorGeneratorCode;
    public string ColorGeneratorName;
    public float generatorAuraPower =150;
    public int sizeOnGrid = 1;

    public int buildTime = 2;
    public bool buildingDone = false;
    public bool generatePower = false;
	// Use this for initialization
	void Start () {
        ColorGeneratorCode = (int)selectedColor;
        ColorGeneratorName = selectedColor.ToString();
        
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
        }
        
        if (buildTime == 0)
        {
            buildingDone = true;       
        }

        if (buildingDone)
        {
            LevelManager.I.auraPower++;
        }
       


    }
}
