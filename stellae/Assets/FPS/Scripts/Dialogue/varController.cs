using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class varController : MonoBehaviour
{
    /*
      This entire class holds the "dialogueTriggered" variable
      It will be accessible to all scripts, such as our activateTrigger and InkDialogue
      
      When a trigger is collided with, activateTrigger sets "dialogueTriggered" to true here
      When InkDialogue pulls the value of "dialogueTriggered", it will get the updated version -- it will be true.

      This script is tied to an empty called "d_trigger". When invoking that gameObject, this variable is accessed.
      "d_trigger" will also be the collider that will manage the activateTrigger script
     */
    
    public bool dialogueTriggered;
    
}
