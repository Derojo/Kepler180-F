using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintPause : MonoBehaviour {

    public GameObject pauseButton, pausePanel;
   
    public void OnPause()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }
    public void OnUnPause()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }



}
