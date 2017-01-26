using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Prefab("ResourceManager", true, "")]
public class ResourceManager : Singleton<ResourceManager>
{
    public float fundings;
    public float powerLevel;
    public float planetHeat;

    private float powerMax;
    public float powerPercentage;


    void OnEnable()
    {
        powerMax = powerLevel;
        updatePowerPercentage();
    }

    public void Load() { return; }

    public void subtractPowerLevel(float amount) {
        Debug.Log("SUBTRACT POWER"+ amount);
        float calc = powerLevel - amount;
        if ((powerLevel - amount) <= 0) {
          
            powerLevel = 0;
            updatePowerPercentage();
        } else {
            powerLevel = powerLevel - amount;
            updatePowerPercentage();
        }
    }

    public void addPowerLevel(float amount) {
        Debug.Log("ADD POWER+"+amount);
        float overload = powerLevel + amount;
        if (overload <= powerMax)
        {
            powerLevel = powerLevel + amount;
            updatePowerPercentage();
        }
        else {
            Debug.Log("powermax");
            powerLevel = powerMax;
        }
    }

    private void updatePowerPercentage() {

        powerPercentage = Mathf.Round((powerLevel / powerMax) * 100);
    }
}

