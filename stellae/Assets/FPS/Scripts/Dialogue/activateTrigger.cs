using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateTrigger : MonoBehaviour
{
    public bool dialogueActive;

    // Start is called before the first frame update
    void Start()
    {
        dialogueActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject x = GameObject.Find("d_trigger");
        varController var = x.GetComponent<varController>();
        var.dialogueTriggered = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // == Set the public dialogueTriggered variable to true == //
        GameObject x = GameObject.Find("d_trigger");
        varController var = x.GetComponent<varController>();
        var.dialogueTriggered = true;
    }
}
