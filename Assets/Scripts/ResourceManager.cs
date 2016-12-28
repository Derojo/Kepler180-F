﻿using System.Collections;
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
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

