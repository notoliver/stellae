using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmonMan : MonoBehaviour
{
    public bool enabled = true;
    public GameObject canvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            canvas.SetActive(!enabled);
            enabled = !enabled;
        }
    }
}
