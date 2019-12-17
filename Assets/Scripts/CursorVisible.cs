using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorVisible : MonoBehaviour
{

    [Tooltip("Whether the cursor is visible, duh")]
    public bool isVisible = true;

    void Update()
    {
        Cursor.visible = isVisible;
    }
}