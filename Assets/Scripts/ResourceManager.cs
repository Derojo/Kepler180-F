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

    public void Load() { return; }

    public void calculatePowerLevel(float amount) {
        float calc = powerLevel - amount;
        if ((powerLevel - amount) <= 0) {
            powerLevel = 0;
        } else {
            powerLevel = powerLevel - amount;
        }

    }
}

