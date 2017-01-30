using System.Collections;
using UnityEngine;

public class SceneHandler : MonoBehaviour {

    public static SceneHandler handler;
    public GameObject loadingImage;

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

    public void ChangeToScene (string scene)
    {
      
        Loader.I.LoadScene(scene);

    }
}
