﻿using System.Collections;
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


    //audio
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
        if (!AudioManager.I.source[1].isPlaying)
        {
            AudioManager.I.source[1].Play();
        }
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
            buildingAuraPower = _buildingAuraPower + ((_buildingAuraPower / 10) * (ColorManager.I.sameColorAmount[cgeneratorTile.clusterId] - 1));
            if (buildingAuraPower > _buildingAuraPower)
            {
                float strengthen = (buildingAuraPower - _buildingAuraPower) / 100;
                gameObject.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().startSize = 2 + strengthen;
            }
        }
        else if (cgeneratorTile.inMixedCluster) {
            StartCoroutine(waitForDetermingAuraPower(cgeneratorTile));
        }
    }

    private IEnumerator waitForDetermingAuraPower(Tile cgeneratorTile) {
        yield return new WaitForSeconds(0.5f);
        float generatorsTurnedOn = 0;
        foreach (Tile tile in ColorManager.I.colorCluster[cgeneratorTile.clusterId].spotTiles)
        {

            BuildingType building = tile.currentObject.GetComponent<BuildingType>();
            if (building.turnedOn)
            {
                generatorsTurnedOn++;
            }
        }
        buildingAuraPower = _buildingAuraPower - ((_buildingAuraPower / 4) * (generatorsTurnedOn - 1));

        float weakening = (_buildingAuraPower - buildingAuraPower) / 100;
        gameObject.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().startSize = 2 - weakening;
    }
}
