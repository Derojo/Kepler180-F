using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubBuilding : MonoBehaviour {

    public GameObject firstParent;
    public GameObject secondParent;

    private bool visible = false;


    public void showSubBuilding() {
        if (visible)
            return;

        if (firstParent.GetComponent<BuildingType>().buildingDone && secondParent.GetComponent<BuildingType>().buildingDone)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.transform.DOMoveY(-10, 1).From().SetEase(Ease.OutCirc);
            visible = true;
        }

    }

}
