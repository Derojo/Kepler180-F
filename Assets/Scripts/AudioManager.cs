using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Prefab("AudioManager", true, "")]
public class AudioManager : Singleton<AudioManager> {

    public AudioSource[] source;


    public void Load() { return; }

 
}
