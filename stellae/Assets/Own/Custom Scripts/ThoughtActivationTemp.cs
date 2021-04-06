using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtActivationTemp : MonoBehaviour
{
    public List<GameObject> objects;
    public GameObject player;
    public string thought;
    bool shot;

    void start()
    {
        shot = false;
    }

    void LateUpdate()
    {
        if (!shot)
        {
            foreach (GameObject ok in objects)
            {
                if (!ok.activeSelf)
                {
                    Debug.Log("inactive");
                    return;
                }
                player.GetComponent<Thought>().Think(thought);
                shot = true;
            }
        }
        
    }
}
