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
        GameObject.Find("GroundHolder").GetComponent<MeshRenderer>().enabled = true;
    }
    public void OnUnPause()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
        GameObject.Find("GroundHolder").GetComponent<MeshRenderer>().enabled = false;
    }



}
