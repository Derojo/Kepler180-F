using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator : MonoBehaviour
{
    public ColorTypes.types selectedColor;
    public int ColorGeneratorCode;
    public string ColorGeneratorName;
    public float auraPower;
    public int sizeOnGrid = 1;

	// Use this for initialization
	void Start () {
        ColorGeneratorCode = (int)selectedColor;
        ColorGeneratorName = selectedColor.ToString();
    }
}
