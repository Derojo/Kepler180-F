using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using RTS_Cam;
public class PlacementController : MonoBehaviour
{
    public GameObject objectInDrag;
    public Vector3 offset;
    public bool startPlacement = false;
    public GameObject BuildingContainer;
    public Color32 selectColor;
    public Color32 busyColor;
    public bool inPlanningMode = false;
    public bool ableToBuild = true;
    public UIManager uimanager;


    private string currentTileScan;

    public TileSelect[] tileSelect;
    private Tile[] lastTileSelected;
    private bool inTween = false;

    void Update()
    {
        if (startPlacement)
        {
            if (tileSelect.Length == 0)
            {
                tileSelect = new TileSelect[1];
                lastTileSelected = new Tile[1];
                tileSelect[0] = new TileSelect();
                tileSelect[0].selectColor = selectColor;
                tileSelect[0].busyColor = busyColor;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfor;

            if (Physics.Raycast(ray, out hitInfor))
            {
                objectInDrag.SetActive(true);
                if (hitInfor.collider.GetComponent<Tile>() != null)
                {

                    ColorGenerator colorinformation = objectInDrag.GetComponent<ColorGenerator>();
                    if (currentTileScan != "" && currentTileScan != hitInfor.collider.name)
                    {
                        currentTileScan = hitInfor.collider.name;
                        ableToBuild = ColorManager.I.AbleToBuildColorGenerator(hitInfor.collider.GetComponent<Tile>(), colorinformation.selectedColor, GetComponent<GridManager>());
                    }
                    // Deselect last hovered tile
                    if (lastTileSelected[0] != null)
                        tileSelect[0].Deselect(lastTileSelected[0]);

                    // Set new on hovered tile and put object in drag on the right tile position
                    lastTileSelected[0] = hitInfor.collider.GetComponent<Tile>();
                    tileSelect[0].Select(lastTileSelected[0], ableToBuild);
                    Vector3 pos = new Vector3(hitInfor.collider.transform.position.x, offset.y, hitInfor.collider.transform.position.z);
                    objectInDrag.transform.position = pos;

                    // Mouse inputs, place or cancel building placement
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (ableToBuild)
                        {
                            PlaceObject();
                        }

                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        CancelPlacement();
                    }

                }

            }
            else
            {
                // If there isn't a ray cast deselect the last tile selected
                objectInDrag.SetActive(false);
                if (lastTileSelected[0] != null)
                {
                    tileSelect[0].Deselect(lastTileSelected[0]);
                }
            }
        }
    }

    public void StartPlacement(GameObject building)
    {
        if (!BuildingManager.I.AbleToBuy(building.GetComponent<BuildingType>()))
        {
            uimanager.ShowMessage(Types.messages.noFunding);

        }
        else
        {
            if (objectInDrag != null)
            {
                objectInDrag.SetActive(false);
                tileSelect[0].Deselect(lastTileSelected[0]);
            }
            GameObject dragObject;
            if (BuildingContainer.transform.Find(building.name) != null)
            {
                dragObject = BuildingContainer.transform.Find(building.name).gameObject;

            }
            else
            {
                dragObject = Instantiate(building) as GameObject;
                dragObject.name = building.name;
                dragObject.transform.parent = BuildingContainer.transform;

            }

            objectInDrag = dragObject;
            startPlacement = true;
        }

    }

    public void CancelPlacement()
    {
        objectInDrag.SetActive(false);
        tileSelect[0].Deselect(lastTileSelected[0]);
        startPlacement = false;
    }

    /* Place the building that is currently in drag if we are in range */
    private void PlaceObject()
    {

        if (lastTileSelected[0].inRange)
        {

            if (PlacementData.I.placementNodes.Count > 0)
            {

                if (lastTileSelected[0].tileType == (int)Types.buildingtypes.colorgenerator)
                {
                    DetermineCluster();
                }

            }

            if (lastTileSelected[0].currentObject != null)
                return;
            GameObject objectInPlace = Instantiate(objectInDrag) as GameObject;
            objectInPlace.transform.position = objectInDrag.transform.position;
            lastTileSelected[0].currentObject = objectInPlace;
            lastTileSelected[0].tileType = (int)objectInPlace.GetComponent<BuildingType>().type;
            objectInPlace.transform.parent = lastTileSelected[0].transform;
            objectInPlace.GetComponent<CapsuleCollider>().enabled = true;
            // Store object with x,z,type and model in placementNodes
            PlacementData.I.AddBuildingNode(lastTileSelected[0].x, lastTileSelected[0].z, objectInPlace.GetComponent<BuildingType>().type, Resources.Load<GameObject>(objectInPlace.GetComponent<BuildingType>().type + "/" + objectInDrag.name), inPlanningMode);
            // Buy building, activate turn on process
            BuildingManager.I.BuyBuilding(objectInPlace.GetComponent<BuildingType>());
            // Check for colorblending
            if (lastTileSelected[0].inMixedCluster)
            {
                List<Tile> blendableTiles = ColorManager.I.getBlendableTiles(lastTileSelected[0], objectInDrag.GetComponent<ColorGenerator>().selectedColor, GetComponent<GridManager>());
                foreach (Tile blendableTile in blendableTiles)
                {
                    Types.colortypes blendableTileColor = blendableTile.currentObject.GetComponent<ColorGenerator>().selectedColor;
                    Types.blendedColors blend = ColorManager.I.getBlendingColor((int)objectInDrag.GetComponent<ColorGenerator>().selectedColor, (int)blendableTileColor);
                    PlaceSubbuildingByColor(blend, lastTileSelected[0], blendableTile);
                }
            }
            // Reinitialize
            CancelPlacement();
            objectInDrag = null;
        }
    }

    private void DetermineCluster()
    {


        Types.colortypes color = objectInDrag.GetComponent<ColorGenerator>().selectedColor;
        Tile adjunctTile = ColorManager.I.getFirstAdjunctTile(lastTileSelected[0], GetComponent<GridManager>());

        if (adjunctTile != null)
        {
            Types.colortypes neighbourColor = adjunctTile.currentObject.GetComponent<ColorGenerator>().selectedColor;

            if (neighbourColor == color)
            {
                int clusterId;


                if (!adjunctTile.inColorCluster)
                {
                    ColorManager.I.sameColorAmount.Add(0);
                    clusterId = (ColorManager.I.sameColorAmount.Count - 1);
                    adjunctTile.colorCluster = color;
                    adjunctTile.inColorCluster = true;
                    adjunctTile.clusterId = clusterId;
                    ColorManager.I.sameColorAmount[clusterId] = ColorManager.I.sameColorAmount[clusterId] + 1;
                }
                else
                {
                    clusterId = adjunctTile.clusterId;
                }
                lastTileSelected[0].colorCluster = color;
                lastTileSelected[0].inColorCluster = true;
                lastTileSelected[0].clusterId = clusterId;
                ColorManager.I.sameColorAmount[clusterId] = ColorManager.I.sameColorAmount[clusterId] + 1;
            }
            else
            {

                // Create mixed color cluster

                if (GetComponent<GridManager>().clusterRestriction)
                {
                    int clusterId;
                    Cluster currentCluster;

                    if (!adjunctTile.inMixedCluster)
                    {
                        ColorManager.I.AddColorCluster();
                        clusterId = ColorManager.I.colorCluster.Count - 1;
                        currentCluster = ColorManager.I.colorCluster[clusterId];
                        adjunctTile.clusterId = clusterId;
                        adjunctTile.inMixedCluster = true;
                        ColorManager.I.fillSpotInCluster(currentCluster, (int)neighbourColor, GetComponent<GridManager>().mixedPlacements);
                    }
                    else
                    {
                        clusterId = adjunctTile.clusterId;
                        currentCluster = ColorManager.I.colorCluster[clusterId];
                    }
                    if (!currentCluster.isFull)
                    {
                        lastTileSelected[0].clusterId = clusterId;
                        lastTileSelected[0].inMixedCluster = true;
                        ColorManager.I.fillSpotInCluster(currentCluster, (int)color, GetComponent<GridManager>().mixedPlacements);
                    }
                }
            }
        }
    }

    private void PlaceSubbuildingByColor(Types.blendedColors color, Tile tileA, Tile tileB)
    {
        BuildingObject blendObject = BuildingManager.I.getObjectByBlend(color);
        if (blendObject != null)
        {
            GameObject subBuilding = Instantiate(blendObject.bObject) as GameObject;
            Vector3 location = new Vector3();
            if (tileA.x == tileB.x)
            { // vertical placement
                location = new Vector3(tileA.transform.position.x, blendObject.offset, (tileA.transform.position.z + tileB.transform.position.z) / 2);
            }
            else
            {
                if (tileA.z == tileB.z)
                { // horizontal placement
                    location = new Vector3((tileA.transform.position.x + tileB.transform.position.x) / 2, blendObject.offset, tileA.transform.position.z);
                }
                else
                { // diagonal placement
                    location = new Vector3((tileA.transform.position.x + tileB.transform.position.x) / 2, blendObject.offset, (tileA.transform.position.z + tileB.transform.position.z) / 2);
                }
            }

            subBuilding.GetComponent<SubBuilding>().firstParent = tileA.currentObject;
            subBuilding.GetComponent<SubBuilding>().secondParent = tileB.currentObject;
            subBuilding.transform.position = location;
            subBuilding.transform.parent = lastTileSelected[0].transform;
        }

    }


}