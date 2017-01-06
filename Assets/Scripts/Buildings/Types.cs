using System.Collections;
using UnityEngine;

[System.Serializable]
public class Types
{
    public enum buildingtypes
    {
        colorgenerator = 0,
        maingenerator = 1,
        // producing minerals for fund increase
        mineraldrill = 2,       // blue - red
        // producing energy
        energygenerator = 3,    // red - yellow
        energytransformer = 4,  // yellow - blue
        // reducing heat
        coolsystem = 5,         // blue - green
    }

    public enum colortypes
    {
        Red = 0,
        Blue = 1,
        Green = 2,
        Yellow = 3
    }

    public enum blendedColors
    {
        Null = 0,
        Purple = 1,
        Brown = 2,
        Orange = 3,
        Lime = 4,
        Green = 5,
        Turquoise = 6
    }

    public enum messages
    {
        noFunding,
        noEnergy
    }

};