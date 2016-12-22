using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {

    public static SceneHandler handler;

    void Awake()
    {
        if (handler == null)
        {
            DontDestroyOnLoad(gameObject);
            handler = this;
        }
        else if (handler != this)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeToScene (string scene) {
        StartCoroutine(LoadSceneIE(scene));
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
