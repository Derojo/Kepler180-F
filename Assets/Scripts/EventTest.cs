using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTest : MonoBehaviour {

    //StartListening 
    void OnEnable()
    {
        EventManager.StartListening ("EndTurn", EndTurnFunction);
    }
    //unregistering listeners for clean up
    void OnDisable()
    {
        EventManager.StopListening("EndTurn", EndTurnFunction);
    }


    void EndTurnFunction()
    {
        Debug.Log("somefunction was called");
    }

}
