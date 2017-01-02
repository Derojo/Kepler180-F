using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Prefab("Loader", true, "")]
public class Loader : Singleton<Loader>
{

    public void LoadScene(string name) {
        StartCoroutine(LoadSceneIE(name));
    }

    public IEnumerator LoadSceneIE(string scene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        while (!async.isDone)
        {
            yield return null;
        }

    }
}
