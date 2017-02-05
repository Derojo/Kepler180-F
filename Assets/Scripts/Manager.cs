using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/* This class wil always be active and will activate other managers */
[Prefab("Manager", true, "")]
public class Manager : Singleton<Manager>
{
    public int levelSelection;
    public void Load() { return; }
    public bool doneTutorial = false;

    void Start()
    {
        PlayerPrefs.SetInt("levelReached", 1);

        LevelManager.I.Load();
        AuraManager.I.Load();
        AudioManager.I.Load();
    }

    public void DetermineLevel(int levelSelection)
    {
        LevelManager.I.currentLevel = levelSelection;
        LevelManager.I.SetCurrentLevel();
        Loader.I.LoadScene("Start");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Planning")
        {
            PlacementData.I.Init();
            //ResourceManager.I.Load();
        }

    }

    public void ChangeToScene(string scene)
    {
        doneTutorial = true;
        Loader.I.LoadScene(scene);
    }

    // Update is called once per frame
    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("closing game");
    }

}