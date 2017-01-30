using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorHandler : MonoBehaviour
{

    public Texture2D defaultCursor;
    private Texture2D currentCursor;
    public Texture2D moving;
    public Texture2D clickable;
    private bool setCursor = false;


    // Use this for initialization

    void Start() {
        //currentCursor = defaultCursor;
    }
    
    void Update()
    {
        /*
            Debug.Log("set raycast");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfor;

            if (Physics.Raycast(ray, out hitInfor))
            {
                string Tag = hitInfor.collider.gameObject.tag;
                if (Tag == "Ground")
                {
                    Debug.Log("ground");
                    setMovingCursor();


                }

                bool useNormalCursorTexture = hitInfor.collider.gameObject.tag.Equals("Ground");

            }
            */

    }
    
    public void setDefaultCursor()
    {
        if (currentCursor != defaultCursor)
        {
            SetCursorTexture(defaultCursor);
            currentCursor = defaultCursor;
        }
    }

    public void setClickableCursor()
    {
        if (currentCursor != clickable)
        {
            SetCursorTexture(clickable);
            currentCursor = clickable;
        }
    }

    public void setMovingCursor() {
        if (currentCursor != moving)
        {
            SetCursorTexture(moving);
            currentCursor = moving;
        }
    }
    void SetCursorTexture(Texture2D tex)
    {
        Cursor.SetCursor(tex, Vector2.zero, CursorMode.Auto);
    }


}
