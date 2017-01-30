using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubBuilding : MonoBehaviour {

    public GameObject firstParent;
    public GameObject secondParent;
    public float harvestPower;
    public float constantEnergyPower;
    public float energyPowerOnce;
    private bool generatedPowerOnce = false;

    public bool visible = false;


    void Start()
    {
        if (!visible) {
            foreach (Renderer render in gameObject.GetComponentsInChildren<Renderer>()) {
                render.enabled = false;
            }
        }
    }

    public void showSubBuilding(bool planning = false) {
        if (visible)
            return;

        if (firstParent.GetComponent<BuildingType>().buildingDone && secondParent.GetComponent<BuildingType>().buildingDone || planning)
        {
            this.gameObject.GetComponent<Collider>().enabled = true;
            this.gameObject.GetComponent<BuildingType>().buildingInfoCanvas.gameObject.SetActive(true);
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


    // MineralDrill specific functions

    public void harvastMinerals()
    {
        Debug.Log("harvest minerals"+ ResourceManager.I.fundings + harvestPower);
        ResourceManager.I.fundings = ResourceManager.I.fundings + harvestPower;
    }


    // Power specific functions

    public void generatePowerOnce()
    {
        if (!generatedPowerOnce) {
            generatedPowerOnce = true;
            ResourceManager.I.addPowerLevel(energyPowerOnce);
        }

    }

    public void generateConstantPower()
    {
        ResourceManager.I.addPowerLevel(constantEnergyPower);
    }

}
