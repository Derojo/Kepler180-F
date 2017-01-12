using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorGenerator : MonoBehaviour
{
    public Types.colortypes selectedColor;
    public int ColorGeneratorCode;
    public string ColorGeneratorName;

    private bool blueprint = false;
    public float buildingAuraPower;
    public int sizeOnGrid = 1;
    private float _buildingAuraPower;

    // Use this for initialization

    void Start()
    {
        blueprint = gameObject.GetComponent<BuildingType>().blueprint;
        _buildingAuraPower = buildingAuraPower;
        ColorGeneratorCode = (int)selectedColor;
        ColorGeneratorName = selectedColor.ToString();
        if (blueprint)
        {
            this.GetComponent<Renderer>().material.renderQueue = 4000;
        }


    }


    public void turnOnGenerator()
    {
        determineAuraPower();
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);

        return;
    }


    public void addAuraPower()
    {
        // Determine aurapower according to cluster groups etc.
        determineAuraPower();
        // Add auraPower to selected colorAuraAmount
        AuraManager.I.AddColorAmount(selectedColor, buildingAuraPower);
        // Calculate positive/negative amount on aura
        AuraManager.I.CalculateAuraPercentage();
    }

    private void determineAuraPower() {
        Tile cgeneratorTile = gameObject.GetComponentInParent<Tile>();
        if (cgeneratorTile.inColorCluster)
        {

            // Aurapower is going to increase since the same colors strengthen the aurapower
            buildingAuraPower = _buildingAuraPower + ((_buildingAuraPower / 4) * (ColorManager.I.sameColorAmount[cgeneratorTile.clusterId] - 1));
            if (buildingAuraPower > _buildingAuraPower)
            {
                float strengthen = (buildingAuraPower - _buildingAuraPower) / 100;
                gameObject.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().startSize = 2 + strengthen;
            }
        }
        else if (cgeneratorTile.inMixedCluster) {
            float amountInCluster = ColorManager.I.colorCluster[cgeneratorTile.clusterId].mixedColorSpots;

            buildingAuraPower = _buildingAuraPower - ((_buildingAuraPower / 4) * (amountInCluster - 1));

            float weakening = (_buildingAuraPower - buildingAuraPower) / 100;
            gameObject.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().startSize = 2 - weakening;
        }
    }
}
