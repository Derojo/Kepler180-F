using UnityEngine;
using System.Collections;

public class DragObjectPlace : MonoBehaviour
{
    public GameObject objectInDrag;
    public Vector3 offset;

    [System.Serializable]
    public class PlaceSelect
    {
        public Color32 selectColor;
        public Color32 busyColor;

        public void Select(PlaceBehaviour place)
        {
            MeshRenderer mesh = place.GetComponent<MeshRenderer>();
            mesh.enabled = true;

            if (place.currentObject == null)
                mesh.material.color = selectColor;
            else
                mesh.material.color = busyColor;

        }

        public void Deselect(PlaceBehaviour place)
        {
            place.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public PlaceSelect placeSelect;
    private PlaceBehaviour lastPlaceSelected;

    public void SetCurrentObjectDrag(GameObject building) {
        objectInDrag = building;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfor;

        if (Physics.Raycast(ray, out hitInfor))
        {
            objectInDrag.SetActive(true);
            if (hitInfor.collider.GetComponent<PlaceBehaviour>() != null)
            {
                if (lastPlaceSelected != null)
                    placeSelect.Deselect(lastPlaceSelected);

                    lastPlaceSelected = hitInfor.collider.GetComponent<PlaceBehaviour>();
                    placeSelect.Select(lastPlaceSelected);
                
                
                if (Input.GetMouseButtonDown(0))
                {
                    if (lastPlaceSelected.currentObject != null)
                        return;

                    GameObject objectInPlace = Instantiate(objectInDrag) as GameObject;
                    objectInPlace.transform.position = objectInDrag.transform.position;
                    lastPlaceSelected.currentObject = objectInPlace;
                    objectInPlace.transform.parent = lastPlaceSelected.transform;
                }

            }
            Vector3 pos = new Vector3(hitInfor.collider.transform.position.x, offset.y, hitInfor.collider.transform.position.z);
            objectInDrag.transform.position = pos;
        }
        else
        {
            objectInDrag.SetActive(false);
            if (lastPlaceSelected != null) {
                placeSelect.Deselect(lastPlaceSelected);
            }
        }
    }
}