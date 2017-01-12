using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/* This class wil always be active and will activate other managers */
[Prefab("Manager", true, "")]
public class Manager : Singleton<Manager>
{

    public void Load() { return; }

    void Start()
    {
        LevelManager.I.Load();
        AuraManager.I.Load();
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
            ResourceManager.I.Load();
        }

    }

    public void ChangeToScene(string scene)
    {
        Loader.I.LoadScene(scene);
    }

    // Update is called once per frame
    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("closing game");
    }
}