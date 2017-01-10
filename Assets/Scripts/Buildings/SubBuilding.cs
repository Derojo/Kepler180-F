using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubBuilding : MonoBehaviour {

    public GameObject firstParent;
    public GameObject secondParent;

    private bool visible = false;

    void Start()
    {

        if (!visible) {
            foreach (Renderer render in gameObject.GetComponentsInChildren<Renderer>()) {
                render.enabled = false;
            }
        }
    }
    public void showSubBuilding() {
        if (visible)
            return;

        if (firstParent.GetComponent<BuildingType>().buildingDone && secondParent.GetComponent<BuildingType>().buildingDone)
        {
            if (gameObject.GetComponent<Renderer>()) {
                gameObject.GetComponent<Renderer>().enabled = true;
            }
            else {
                foreach (Renderer render in gameObject.GetComponentsInChildren<Renderer>())
                {
                    render.enabled = true;
                }
            }
            gameObject.transform.DOMoveY(-10, 1).From().SetEase(Ease.OutCirc);
            visible = true;
        }

    }

}
