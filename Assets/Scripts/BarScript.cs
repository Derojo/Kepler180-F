using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	[SerializeField]
	private float fillAmount;

	[SerializeField]
	private Image content;

	public float MaxValue { get; set; }

	public float Value
	{
		set
		{ 
			fillAmount = Map (value, 0, MaxValue, 0, 1);
		}
	}

	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () 
	{
		HandleBar ();
	}

	private void HandleBar()
	{

		content.fillAmount = LevelManager.I.auraLevelPercentage;
	}

	private float Map(float value, float inMin, float inMax, float outMin, float outMax)
	{
		return (LevelManager.I.auraLevelPercentage - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
		// Als er bijvoorbeeld 79 procent aurapercentage is wordt dit verwerkt naar 0,79
		// (79 - 0) * (1 - 0) / (100 - 0) + 0;
		// 	  80 * 1 / 100 + 0 = 0,8 
	}
}
