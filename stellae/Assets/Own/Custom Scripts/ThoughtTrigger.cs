using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtTrigger : MonoBehaviour
{
    public bool repeatable, triggered;
    public string thought;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (repeatable)
        {
            player.GetComponent<Thought>().Think(thought);
        }
        else
        {
            if (!triggered)
            {
                player.GetComponent<Thought>().Think(thought);
                triggered = true;
            }
        }
    }
}
