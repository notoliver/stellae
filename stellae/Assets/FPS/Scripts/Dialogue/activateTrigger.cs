using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
      This entire class holds the "dialogueTriggered" variable
      It will be accessible to all scripts, specifically InkDialogue
      
      When a trigger is collided with, activateTrigger sets "dialogueTriggered" to true here
      When InkDialogue pulls the value of "dialogueTriggered", it will get the updated version -- it will be true.

      This script is tied to an empty called "d_trigger". When invoking that gameObject, this variable is accessed.

      Delaying the update of dialogueTriggered to false is necessary. If not, it will be updated to false before being
      accessed by InkDialogue every time. Therefore, that would make the OnTriggerEnter function useless. A delay gives
      InkDialogue enough time to read dialogueTriggered as "true" and make the dialogue pop up before the signal to
      revert it to "false" goes through from the Update() function.
      
      Setting it false currently doesn't disable dialogue. This may be adjusted in a future update.
     */

public class activateTrigger : MonoBehaviour
{
    public bool dialogueTriggered;
    // == For delaying Update() == //
    int i = 0;



    // Start is called before the first frame update
    void Start()
    {
        dialogueTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        // == Set the dialogueTriggered to be false after 10 frames == //
        if (i < 10)
        {
            i++;
        }
        else if (i == 10)
        {
            dialogueTriggered = false;
            i = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // == Set dialogueTriggered variable to true == //
        dialogueTriggered = true;
        Debug.Log("dialogue triggered now true");
    }
}
