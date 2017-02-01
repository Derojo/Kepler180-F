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
    public float reservePower = 0;
    public float powerPercentage;


    void OnEnable()
    {

        powerMax = powerLevel;
        updatePowerPercentage();
    }

    public void Load() { return; }
    public void setTutorialValues()
    {
        powerLevel = 2500;
        powerMax = 2500;
        powerPercentage = 100;
        fundings = 400;
    }
    public void subtractPowerLevel(float amount) {
        float calc = powerLevel - amount;
        if ((powerLevel - amount) <= 0) {
          
            powerLevel = 0;
            updatePowerPercentage();
        } else {
            if(reservePower > amount)
            {
                reservePower = reservePower - amount;
            }
            powerLevel = powerLevel - amount;
            updatePowerPercentage();
        }
    }

    public void addPowerLevel(float amount) {
        float overload = powerLevel + amount;
        if (overload <= powerMax)
        {
            powerLevel = powerLevel + amount;
            updatePowerPercentage();
        }
        else {
            reservePower+= overload - powerMax;
            powerLevel = powerMax;
            updatePowerPercentage();
        }
    }

    private void updatePowerPercentage() {

        powerPercentage = Mathf.Round((powerLevel / powerMax) * 100);
    }
}

