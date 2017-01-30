using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectorScript : MonoBehaviour {


    public Button[] levelButtons;
    public GameObject loadingImage;


    void Start()
    {
        
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
        }
    }
	// Update is called once per frame

}
