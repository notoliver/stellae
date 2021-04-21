using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
   void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }
   public void doExitGame()
        {
            Application.Quit();
        }
}
