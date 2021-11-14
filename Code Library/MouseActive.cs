using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseActive : MonoBehaviour
{
    
    
    // activates mouse and make it visible
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
