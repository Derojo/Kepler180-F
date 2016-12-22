using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragObjectPlace : MonoBehaviour
{
    public GameObject objectInDrag;
    public Vector3 offset;
    public bool startPlacement = false;
    public GameObject BuildingContainer;
    public Color32 selectColor;
    public Color32 busyColor;
    public bool inPlanningMode = false;

    [System.Serializable]
    public class TileSelect
    {
        public Color32 selectColor;
        public Color32 busyColor;

        public void Select(Tile tile, int sizeOnGrid = 1)
        {
            MeshRenderer mesh = tile.GetComponent<MeshRenderer>();
            mesh.enabled = true;
            /*
            if (sizeOnGrid > 1)
            {
                string[] placecoordinates = place.name.Split(new string[] { "|" }, System.StringSplitOptions.None); ;
                int x = int.Parse(placecoordinates[0]);
                int z = int.Parse(placecoordinates[1]);
                if (place.currentObject == null) {
                    if ((z - 1) > 0) {
                        z--;
                    }
                    if (Grid.getGridCellAtPosition(x, z).GetComponent<Tile>().currentObject != null) {
                        mesh.material.color = busyColor;
                        if (Grid.getGridCellAtPosition(x, (int.Parse(placecoordinates[1])+1)).GetComponent<Tile>().currentObject == null) {
                            mesh.material.color = selectColor;
                        }
                    }
                    else {
                        mesh.material.color = selectColor;
                    }
                } else {
                    mesh.material.color = busyColor;
                    
                }

            }*/
            // else {
            if (tile.inRange)
            {
                if (tile.currentObject == null)
                {
                    mesh.material.color = selectColor;
                    mesh.material.SetColor("_EmissionColor", (Color)selectColor* 0.3f);
                } else {
                    mesh.material.color = busyColor;
                    mesh.material.SetColor("_EmissionColor", (Color)busyColor * 0.3f);

                }
                    
            }
            else {
                mesh.material.color = busyColor;
                mesh.material.SetColor("_EmissionColor", (Color)busyColor * 0.3f);
            }

            //}


        }

        public void Deselect(Tile tile)
        {
            tile.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public TileSelect[] tileSelect;
    private Tile[] lastTileSelected;

    void Update()
    {
        if (startPlacement)
        {
            /*
            if (objectInDrag.GetComponent<ColorGenerator>().sizeOnGrid > 1)
            {
                if (tileSelect.Length == 0 || tileSelect.Length < objectInDrag.GetComponent<ColorGenerator>().sizeOnGrid)
                {
                    tileSelect = new TileSelect[objectInDrag.GetComponent<ColorGenerator>().sizeOnGrid];
                    lastTileSelected = new Tile[objectInDrag.GetComponent<ColorGenerator>().sizeOnGrid];
                    tileSelect[0] = new TileSelect();
                    tileSelect[0].selectColor = selectColor;
                    tileSelect[0].busyColor = busyColor;
                    tileSelect[1] = new TileSelect();
                    tileSelect[1].selectColor = selectColor;
                    tileSelect[1].busyColor = busyColor;
                }
            }*/
            //else {
                if (tileSelect.Length == 0)
                {
                    tileSelect = new TileSelect[1];
                    lastTileSelected = new Tile[1];
                    tileSelect[0] = new TileSelect();
                    tileSelect[0].selectColor = selectColor;
                    tileSelect[0].busyColor = busyColor;
                }
           // }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfor;

            if (Physics.Raycast(ray, out hitInfor))
            {
                objectInDrag.SetActive(true);
                if (hitInfor.collider.GetComponent<Tile>() != null)
                {
                    //checkIfInRange(hitInfor.collider.gameObject);
                    ColorGenerator colorinformation = objectInDrag.GetComponent<ColorGenerator>();
                    /*
                    if (colorinformation.sizeOnGrid > 1)
                    {

                        // Place building with size 2
                        
                        if (colorinformation.sizeOnGrid == 2) {
                            // Get x,z values of hitcollider placement grid item
                            string[] placecoordinates = hitInfor.collider.name.Split(new string[] { "|" }, System.StringSplitOptions.None);
                            int x = int.Parse(placecoordinates[0]);
                            int z = int.Parse(placecoordinates[1]);
                            if (z != (Grid.zMax - 1))
                            {
                                z++;
                            }
                            for (int i = 0; i < 2; i++) {
                                if (lastTileSelected[i] != null)
                                    tileSelect[i].Deselect(lastTileSelected[i]);
                                if (i == 0) {
                                    lastTileSelected[i] = hitInfor.collider.GetComponent<Tile>();
                                }
                                else if (i == 1) {
                                    lastTileSelected[1] = Grid.getGridCellAtPosition(x, z).GetComponent<Tile>();
                                }
                                tileSelect[i].Select(lastTileSelected[i], colorinformation.sizeOnGrid);
                            }
                            float centerZ = (hitInfor.collider.transform.position.z + Grid.getGridCellAtPosition(x, z).transform.position.z) / 2;
                            Vector3 pos = new Vector3(hitInfor.collider.transform.position.x, offset.y, centerZ);
                            objectInDrag.transform.position = pos;
                        }
                    } else {*/

                        if (lastTileSelected[0] != null)
                            tileSelect[0].Deselect(lastTileSelected[0]);

                        lastTileSelected[0] = hitInfor.collider.GetComponent<Tile>();
                        tileSelect[0].Select(lastTileSelected[0]);
                        Vector3 pos = new Vector3(hitInfor.collider.transform.position.x, offset.y, hitInfor.collider.transform.position.z);
                        objectInDrag.transform.position = pos;
                    //}


                    
                    if (Input.GetMouseButtonDown(0))
                    {
                        /*
                    if (colorinformation.sizeOnGrid > 1) {
                        if (lastTileSelected[0].currentObject != null)
                            return;
                        GameObject objectInPlace = Instantiate(objectInDrag) as GameObject;
                        objectInPlace.transform.position = objectInDrag.transform.position;

                        for (int i = 0; i < colorinformation.sizeOnGrid; i++) {
                            lastTileSelected[i].currentObject = objectInPlace;
                        }
                        objectInPlace.transform.parent = lastTileSelected[0].transform;
                    }
                    else {*/
                        if (lastTileSelected[0].inRange) {
                            Debug.Log("in range");
                            if (lastTileSelected[0].currentObject != null)
                                return;
                            GameObject objectInPlace = Instantiate(objectInDrag) as GameObject;
                            objectInPlace.transform.position = objectInDrag.transform.position;
                            lastTileSelected[0].currentObject = objectInPlace;
                            lastTileSelected[0].tileType = (int)objectInPlace.GetComponent<BuildingType>().type;
                            objectInPlace.transform.parent = lastTileSelected[0].transform;
                            // Store object with x,z,type and model in placementNodes
                            PlacementData.I.AddBuildingNode(lastTileSelected[0].x, lastTileSelected[0].z, objectInPlace.GetComponent<BuildingType>().type, Resources.Load<GameObject>(objectInPlace.GetComponent<BuildingType>().type+"/"+objectInDrag.name), inPlanningMode);
                            CancelPlacement();
                            objectInDrag = null;
                        }
                        //}
                    }
                    else if (Input.GetMouseButtonDown(1)) {
                        CancelPlacement();
                    }

                }

            }
            else
            {
                objectInDrag.SetActive(false);
                for (int i = 0; i < objectInDrag.GetComponent<ColorGenerator>().sizeOnGrid; i++ ) {
                    if (lastTileSelected[i] != null)
                    {
                        tileSelect[i].Deselect(lastTileSelected[i]);
                    }
                }

            }
        }
    }

    public void StartPlacement(GameObject building)
    {
        if (objectInDrag != null) {
            objectInDrag.SetActive(false);
            tileSelect[0].Deselect(lastTileSelected[0]);
        }
        GameObject dragObject;
        if (BuildingContainer.transform.Find(building.name) != null) {
            dragObject = BuildingContainer.transform.Find(building.name).gameObject;

        } else {
            dragObject = Instantiate(building) as GameObject;
            dragObject.name = building.name;
            dragObject.transform.parent = BuildingContainer.transform;

        }
        
        objectInDrag = dragObject;
        startPlacement = true;
    }

    public void CancelPlacement()
    {
        
        objectInDrag.SetActive(false);
        tileSelect[0].Deselect(lastTileSelected[0]);
        startPlacement = false;
    }

}