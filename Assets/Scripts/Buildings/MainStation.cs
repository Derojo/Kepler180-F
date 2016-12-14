using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStation : MonoBehaviour {

    public GameObject station;
    public GridContainer grid;

    public int range;
	// Use this for initialization
	void Start () {
        StartCoroutine(waitForPlacement(1));
    }
    

    private void addMainStation()
    {
        //int sizeX = 2;
        //int sizeZ = 2;
        int startX = (Grid.xMax / 2) - 1;
        int startZ = (Grid.zMax / 2) - 1;

        // Place gameobject under first cell
        GameObject firstcell = Grid.getGridCellAtPosition(startX, startZ);
        station.transform.parent = firstcell.transform;
        //float centerZ = (firstcell.transform.position.z + Grid.getGridCellAtPosition(startX, startZ+1).transform.position.z) / 2;
        //float centerX = (firstcell.transform.position.x + Grid.getGridCellAtPosition(startX+1, startZ).transform.position.x) / 2;
        Vector3 pos = new Vector3(firstcell.transform.position.x, station.transform.position.y, firstcell.transform.position.z);
        firstcell.GetComponent<Tile>().currentObject = station;
        firstcell.GetComponent<Tile>().tileType = (int)this.GetComponent<BuildingType>().type;
        station.transform.position = pos;
        station.SetActive(true);
        grid.updateRange(startX, startZ, range);
        /*
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                GameObject cell = Grid.getGridCellAtPosition(startX + x, startZ + z);
                cell.GetComponent<Tile>().currentObject = station;
            }

        }*/
    }

    public IEnumerator waitForPlacement(float time) {
        if (!grid.gridReady) {
            yield return null;
        }

        addMainStation();
    }
}
