using UnityEngine;
using System.Collections;

public class DragObjectPlace : MonoBehaviour
{
    public GameObject objectInDrag;
    public Vector3 offset;
    //public PlaceSelect placeSelect;

    private PlaceBehaviour lastPlaceSelected;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfor;

        if (Physics.Raycast(ray, out hitInfor))
        {
            Debug.Log(hitInfor.collider.gameObject);
            objectInDrag.SetActive(true);

            Vector3 pos = new Vector3(hitInfor.collider.transform.position.x, offset.y, hitInfor.collider.transform.position.z);
            objectInDrag.transform.position = pos;
        }
        else
        {
            objectInDrag.SetActive(false);
        }
    }
}
