using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintObject : MonoBehaviour {


    void OnEnable()
    {
        EventManager.StartListening("InBlueprint", ActivateObject);
        EventManager.StartListening("OutBlueprint", DeactivateObject);
    }
    //unregistering listeners for clean up
    void OnDisable()
    {
        EventManager.StopListening("InBlueprint", ActivateObject);
        EventManager.StartListening("OutBlueprint", DeactivateObject);
    }
    // Use this for initialization
    void ActivateObject() {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    void DeactivateObject() {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

}
