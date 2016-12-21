using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class wil always be active and will activate other managers */
[Prefab("GameManager", true, "")]
public class GameManager : Singleton<GameManager> {

	// Use this for initialization
	void Start () {
        PlacementData.I.Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
