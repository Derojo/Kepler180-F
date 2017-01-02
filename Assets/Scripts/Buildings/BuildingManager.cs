using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Prefab("BuildingManager", true, "")]
public class BuildingManager : Singleton<BuildingManager> { 

    [System.Serializable]
    public class BuildingObject {
        public Types.blendedColors blendedColor;
        public GameObject bObject;
    }

    public BuildingObject[] buildingObjects;


    public GameObject getObjectByBlend(Types.blendedColors blendedColor) {


        foreach (BuildingObject o in buildingObjects) {
            if (o.blendedColor == blendedColor) {
                return o.bObject;
            }
        }

        return null;
    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
