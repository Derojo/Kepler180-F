using System.Collections;
using UnityEngine;

[System.Serializable]
public class TileSelect
{
    public Color32 selectColor;
    public Color32 busyColor;

    public void Select(Tile tile, bool ableToBuild)
    {
        MeshRenderer mesh = tile.GetComponent<MeshRenderer>();
        mesh.enabled = true;

        if (tile.inRange)
        {
            if (tile.currentObject == null && ableToBuild)
            {
                mesh.material.color = selectColor;
                mesh.material.SetColor("_EmissionColor", (Color)selectColor * 0.3f);
            }
            else
            {
                mesh.material.color = busyColor;
                mesh.material.SetColor("_EmissionColor", (Color)busyColor * 0.3f);

            }

        }
        else
        {
            mesh.material.color = busyColor;
            mesh.material.SetColor("_EmissionColor", (Color)busyColor * 0.3f);
        }

    }

    public void Deselect(Tile tile)
    {
        tile.GetComponent<MeshRenderer>().enabled = false;
    }
}
