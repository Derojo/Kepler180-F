using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour {

    public Texture2D defaultCursor;
    public Texture2D currentCursor;
    public Texture2D moving;
    private bool setCursor = false;

    // Use this for initialization

    void Start() {
        currentCursor = defaultCursor;
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfor;

        if (Physics.Raycast(ray, out hitInfor))
        {
            string Tag = hitInfor.collider.gameObject.tag;
            if (Tag == "Ground")
            {
                if (currentCursor != moving) {
                    SetCursorTexture(moving);
                    currentCursor = moving;
                }
                
            }
            else {
                if (currentCursor != defaultCursor)
                {
                    SetCursorTexture(defaultCursor);
                    currentCursor = defaultCursor;
                }
                
            }

            bool useNormalCursorTexture = hitInfor.collider.gameObject.tag.Equals("Ground");
            
        }
    }

    void SetCursorTexture(Texture2D tex)
    {
        Cursor.SetCursor(tex, Vector2.zero, CursorMode.Auto);
    }


}
