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
		if (clickCounter + 1 <= 8)
		{
			clickCounter++;
		}
			ShowTutorialImages ();
	}

	public void ClickBackwards(){
		if (clickCounter - 1 >= 0)
		{
			clickCounter--;
		}

		ShowTutorialImages ();
	}



	public void ShowTutorialImages()
	{
		if(clickCounter == 1)
		{
			gameObject.transform.GetChild(0).gameObject.SetActive(true);
			gameObject.transform.GetChild(1).gameObject.SetActive(false);
		}

		if (clickCounter == 2) {
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
			gameObject.transform.GetChild(1).gameObject.SetActive(true);
			gameObject.transform.GetChild(2).gameObject.SetActive(false);
		}

		if (clickCounter == 3) {
			gameObject.transform.GetChild(1).gameObject.SetActive(false);
			gameObject.transform.GetChild(2).gameObject.SetActive(true);
			gameObject.transform.GetChild(3).gameObject.SetActive(false);
		}

		if (clickCounter == 4) {
			gameObject.transform.GetChild(2).gameObject.SetActive(false);
			gameObject.transform.GetChild(3).gameObject.SetActive(true);
			gameObject.transform.GetChild(4).gameObject.SetActive(false);
		}

		if (clickCounter == 5) {
			gameObject.transform.GetChild(3).gameObject.SetActive(false);
			gameObject.transform.GetChild(4).gameObject.SetActive(true);
			gameObject.transform.GetChild(5).gameObject.SetActive(false);
		}

		if (clickCounter == 6) {
			gameObject.transform.GetChild(4).gameObject.SetActive(false);
			gameObject.transform.GetChild(5).gameObject.SetActive(true);
			gameObject.transform.GetChild(6).gameObject.SetActive(false);
		}

		if (clickCounter == 7) {
			gameObject.transform.GetChild(5).gameObject.SetActive(false);
			gameObject.transform.GetChild(6).gameObject.SetActive(true);
			gameObject.transform.GetChild(7).gameObject.SetActive(false);
		}

		if (clickCounter == 8) {
			gameObject.transform.GetChild(6).gameObject.SetActive(false);
			gameObject.transform.GetChild(7).gameObject.SetActive(true);
			gameObject.transform.GetChild(8).gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
