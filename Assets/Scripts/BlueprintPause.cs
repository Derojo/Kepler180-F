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
    }
    public void OnUnPause()
    {
        BlueprintManager.I.inBluePrint = true;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }



}
