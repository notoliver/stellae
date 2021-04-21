using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool paused = false;
    public GameObject pause;
    public AudioSource source;
    public AudioClip open;
    public AudioClip close;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            paused = togglePause();
    }

    /*void OnGUI()
    {
        if (paused)
        {
            GUILayout.Label("Game is paused!");
            if (GUILayout.Button("Click me to unpause"))
                paused = togglePause();
        }
    }*/

    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            source.PlayOneShot(close, 0.5f);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            pause.SetActive(false);
            return (false);
        }
        else
        {
            source.PlayOneShot(open,0.5f);
            Cursor.lockState = CursorLockMode.None;
            pause.SetActive(true);
            Time.timeScale = 0f;
            //update bird shit
            GameObject.Find("birdsJ").GetComponent<Text>().text = GameObject.Find("birdinventory").GetComponent<PermaInv>().text;
            if (GameObject.Find("birdinventory").GetComponent<PermaInv>().normal){
                GameObject.Find("birdpatch").GetComponent<Image>().enabled = true;
            }
            if (GameObject.Find("birdinventory").GetComponent<PermaInv>().rare)
            {
                GameObject.Find("rarebirdpatch").GetComponent<Image>().enabled = true;
            }
            //update tent badge
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "2_Freeroam1")
            {
                if(GameObject.Find("===== TRIGGERS =====").GetComponent<ThoughtActivationTemp>().shot)
                {
                    GameObject.Find("tentpatch").GetComponent<Image>().enabled = true;
                }
            }

                return (true);
        }
    }
}
