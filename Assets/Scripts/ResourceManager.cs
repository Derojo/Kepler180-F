using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Prefab("ResourceManager", true, "")]
public class ResourceManager : Singleton<ResourceManager>
{
    public float fundings = 5000;
    public float powerLevel = 2000;
    public float planetHeat;

}
