using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintPause : MonoBehaviour {

    public GameObject pauseButton, pausePanel;
   
    public void OnPause()
    {
        BlueprintManager.I.inBluePrint = true;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        GameObject.Find("GroundHolder").GetComponent<MeshRenderer>().enabled = true;
    }
    public void OnUnPause()
    {
        BlueprintManager.I.inBluePrint = true;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        GameObject.Find("GroundHolder").GetComponent<MeshRenderer>().enabled = false;
    }



}
