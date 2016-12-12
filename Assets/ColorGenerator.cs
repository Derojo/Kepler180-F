using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator : MonoBehaviour
{
    [System.Serializable]
    public class ColorTypes
    {
        public enum types {
            Red = 0,
            Blue = 1,
            Green = 2,
            Yellow = 3
        }
    };

    public ColorTypes.types selectedColor;
    public int ColorGeneratorCode;
    public string ColorGeneratorName;
    public int sizeOnGrid = 1;

	// Use this for initialization
	void Start () {
        ColorGeneratorCode = (int)selectedColor;
        ColorGeneratorName = selectedColor.ToString();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
