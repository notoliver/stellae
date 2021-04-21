using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public List<GameObject> objects;
    public string sceneName, moreToDo;
    public GameObject player;
    public string thing;
    public bool inv;

    private void OnTriggerEnter(Collider other)
    {
        if (inv)
        {
            if (player.GetComponent<Inventory>().inventory.Contains(thing))
            {
                StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, sceneName));
            }
            else
            {
                player.GetComponent<Thought>().Think(moreToDo);
            }
        }
        else
        {
            foreach (GameObject thing in objects)
            {
                if (thing.active)
                {
                    continue;
                }
                else
                {
                    player.GetComponent<Thought>().Think(moreToDo);
                    return;
                }
            }
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, sceneName));
        }   
    }
}
