using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Derojo.Tutorial
{
    [System.Serializable]
    public class Step
    {
        public bool showSettings = false;
        public string title = "";
        public int id;
        public int nextStep;
        public float delay;
        public bool enableCam;
        public bool hideWindow;
        public bool highlightUI;
        public GameObject UIobject;
        public GameObject UImask;
        public bool fadeButton = true;
        public bool inFade = true;
        public bool outFade;
        public bool highlightObject;
        public GameObject highLightGameObject;
        public bool moveToTargetObject;
        public float zoomPos;
        public float zoomSpeed;
        public float zoomOffsetZ;
        public string highlightTile = "";
        public string unlockTile = "";
        public bool turnOffLights;
        public bool turnOnLightsAfter;
        public string afterFunction = "";
        public bool hasAction;
        public GameObject actionMessage;
        public string eventName = "";

        public Step(int _id) {
            id = _id;
        }
    }
}