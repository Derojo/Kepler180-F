using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStation : MonoBehaviour {

    public GameObject station;
    public GridManager grid;
    public float offset;

    public int range;
	// Use this for initialization
	void Start () {
        StartCoroutine(waitForPlacement(1));
    }
    

    private void addMainStation()
    {
        int startX = (Grid.xMax / 2) - 1;
        int startZ = (Grid.zMax / 2) - 1;

        GameObject firstcell = Grid.getGridCellAtPosition(startX, startZ);
        firstcell.tag = "Clickable";
        station.transform.parent = firstcell.transform;
        Vector3 pos = new Vector3(firstcell.transform.position.x, (station.transform.position.y+offset), firstcell.transform.position.z);
        firstcell.GetComponent<Tile>().currentObject = station;
        firstcell.GetComponent<Tile>().tileType = (int)this.GetComponent<BuildingType>().type;
        station.transform.position = pos;
        station.SetActive(true);
        grid.updateRange(startX, startZ, range);
    }

    public IEnumerator waitForPlacement(float time) {
        if (!grid.gridReady) {
            yield return null;
        }

        addMainStation();
    }
}
