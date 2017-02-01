using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Manager.I.Load();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartMenuButtons()
    {
        AudioManager.I.source[6].Play();
    }

    public void ChangeToScene(string scene)
    {
        if (scene == "LevelSelectionScene")
        {
            if (!Manager.I.doneTutorial)
            {
                LevelManager.I.setTutorialLevel();
                scene = "Tutorial";
            }
            else
            {
                scene = "LevelSelectionScene";
            }
        }
        Loader.I.LoadScene(scene);
    }
}
