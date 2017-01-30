using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StaticTut : MonoBehaviour {
	public int clickCounter;

	// Use this for initialization
	void Start () {
		clickCounter = 1;
		ShowTutorialImages();
	}

	public void ClickForward(){
		clickCounter++;
		ShowTutorialImages ();
	}

	public void ClickBackwards(){
		clickCounter--;
		ShowTutorialImages ();
	}



	public void ShowTutorialImages()
	{
		if(clickCounter == 1)
		{
			gameObject.transform.GetChild(0).gameObject.SetActive(true);
		}

		if (clickCounter == 2) {
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
			gameObject.transform.GetChild(1).gameObject.SetActive(true);
			gameObject.transform.GetChild(2).gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
