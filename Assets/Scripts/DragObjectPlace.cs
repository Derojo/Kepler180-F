using UnityEngine;
using System.Collections;

public class DragObjectPlace : MonoBehaviour
{
    public GameObject objectInDrag;
    public Vector3 offset;
    public bool startPlacement = false;
    public GameObject BuildingContainer;
    public Color32 selectColor;
    public Color32 busyColor;

    [System.Serializable]
    public class PlaceSelect
    {
        public Color32 selectColor;
        public Color32 busyColor;

        public void Select(PlaceBehaviour place, int sizeOnGrid = 1)
        {
            MeshRenderer mesh = place.GetComponent<MeshRenderer>();
            mesh.enabled = true;

            if (sizeOnGrid > 1)
            {
                string[] placecoordinates = place.name.Split(new string[] { "|" }, System.StringSplitOptions.None); ;
                int x = int.Parse(placecoordinates[0]);
                int z = int.Parse(placecoordinates[1]);
                if (place.currentObject == null) {
                    if ((z - 1) > 0) {
                        z--;
                    }
                    if (Grid.getGridCellAtPosition(x, z).GetComponent<PlaceBehaviour>().currentObject != null) {
                        mesh.material.color = busyColor;
                        if (Grid.getGridCellAtPosition(x, (int.Parse(placecoordinates[1])+1)).GetComponent<PlaceBehaviour>().currentObject == null) {
                            mesh.material.color = selectColor;
                        }
                    }
                    else {
                        mesh.material.color = selectColor;
                    }
                } else {
                    mesh.material.color = busyColor;
                    
                }

            }
            else {
                if (place.currentObject == null)
                    mesh.material.color = selectColor;
                else
                    mesh.material.color = busyColor;
            }


        }

        public void Deselect(PlaceBehaviour place)
        {
            place.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public PlaceSelect[] placeSelect;
    private PlaceBehaviour[] lastPlaceSelected;

    void Update()
    {
        if (startPlacement)
        {
            if (objectInDrag.GetComponent<ColorGenerator>().sizeOnGrid > 1)
            {
                if (placeSelect.Length == 0 || placeSelect.Length < objectInDrag.GetComponent<ColorGenerator>().sizeOnGrid)
                {
                    placeSelect = new PlaceSelect[objectInDrag.GetComponent<ColorGenerator>().sizeOnGrid];
                    lastPlaceSelected = new PlaceBehaviour[objectInDrag.GetComponent<ColorGenerator>().sizeOnGrid];
                    placeSelect[0] = new PlaceSelect();
                    placeSelect[0].selectColor = selectColor;
                    placeSelect[0].busyColor = busyColor;
                    placeSelect[1] = new PlaceSelect();
                    placeSelect[1].selectColor = selectColor;
                    placeSelect[1].busyColor = busyColor;
                }
            }
            else {
                if (placeSelect.Length == 0)
                {
                    placeSelect = new PlaceSelect[1];
                    lastPlaceSelected = new PlaceBehaviour[1];
                    placeSelect[0] = new PlaceSelect();
                    placeSelect[0].selectColor = selectColor;
                    placeSelect[0].busyColor = busyColor;
                }
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfor;

            if (Physics.Raycast(ray, out hitInfor))
            {
                objectInDrag.SetActive(true);
                if (hitInfor.collider.GetComponent<PlaceBehaviour>() != null)
                {
                    ColorGenerator colorinformation = objectInDrag.GetComponent<ColorGenerator>();
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
                                if (lastPlaceSelected[i] != null)
                                    placeSelect[i].Deselect(lastPlaceSelected[i]);
                                if (i == 0) {
                                    lastPlaceSelected[i] = hitInfor.collider.GetComponent<PlaceBehaviour>();
                                }
                                else if (i == 1) {
                                    lastPlaceSelected[1] = Grid.getGridCellAtPosition(x, z).GetComponent<PlaceBehaviour>();
                                }
                                placeSelect[i].Select(lastPlaceSelected[i], colorinformation.sizeOnGrid);
                            }
                            float centerZ = (hitInfor.collider.transform.position.z + Grid.getGridCellAtPosition(x, z).transform.position.z) / 2;
                            Vector3 pos = new Vector3(hitInfor.collider.transform.position.x, offset.y, centerZ);
                            objectInDrag.transform.position = pos;
                        }
                    } else {

                        if (lastPlaceSelected[0] != null)
                            placeSelect[0].Deselect(lastPlaceSelected[0]);

                        lastPlaceSelected[0] = hitInfor.collider.GetComponent<PlaceBehaviour>();
                        placeSelect[0].Select(lastPlaceSelected[0]);
                        Vector3 pos = new Vector3(hitInfor.collider.transform.position.x, offset.y, hitInfor.collider.transform.position.z);
                        objectInDrag.transform.position = pos;
                    }


                    
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (colorinformation.sizeOnGrid > 1) {
                            if (lastPlaceSelected[0].currentObject != null)
                                return;
                            GameObject objectInPlace = Instantiate(objectInDrag) as GameObject;
                            objectInPlace.transform.position = objectInDrag.transform.position;
                            
                            for (int i = 0; i < colorinformation.sizeOnGrid; i++) {
                                lastPlaceSelected[i].currentObject = objectInPlace;
                            }
                            objectInPlace.transform.parent = lastPlaceSelected[0].transform;
                        }
                        else {
                            if (lastPlaceSelected[0].currentObject != null)
                                return;
                            GameObject objectInPlace = Instantiate(objectInDrag) as GameObject;
                            objectInPlace.transform.position = objectInDrag.transform.position;
                            lastPlaceSelected[0].currentObject = objectInPlace;
                            objectInPlace.transform.parent = lastPlaceSelected[0].transform;
                        }



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
                    if (lastPlaceSelected[i] != null)
                    {
                        placeSelect[i].Deselect(lastPlaceSelected[i]);
                    }
                }

            }
        }
    }

    public void StartPlacement(GameObject building)
    {
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
        placeSelect[0].Deselect(lastPlaceSelected[0]);
        startPlacement = false;
    }

}