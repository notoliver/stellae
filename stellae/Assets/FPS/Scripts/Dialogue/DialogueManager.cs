using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public NPC npc;

    bool isTalking = false;

    float distance;
    // Organize which response needs to be generated, based off of player dialogue
    float curResponseTracker = 0;

    public GameObject player;
    public GameObject dialogueUI;

    // Make text objects
    public Text npcName;
    public Text npcDialogueBox;
    public Text playerResponse;


    // Start is called before the first frame update
    void Start()
    {
        // Only want UI to pop up when we are talking with NPCs
        dialogueUI.SetActive(false);
    }

    // Only want method to trigger if we're close enough to the NPC
    void OnMouseOver()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        if (distance <= 10f)
        {
            // *** Use the scroll wheel to scroll through dialogue options *** //

            // Scrolling down
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                curResponseTracker++;

                // Bounding the scroll to keep you from scrolling past the final dialogue option
                if (curResponseTracker >= npc.playerDialogue.Length - 1)
                {
                    curResponseTracker = npc.playerDialogue.Length - 1;
                }
            }

            // Scrolling up
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                curResponseTracker--;

                // Bounding the scroll to top you from scrolling "before" the first dialogue option
                if (curResponseTracker < 0)
                {
                    curResponseTracker = 0;
                }
            }

            // Trigger our dialogue (press E)
            if (Input.GetKeyDown(KeyCode.E) && isTalking == false)
            {
                StartConversation();
            }
            // Leave the conversation by pressing E again
            else if (Input.GetKeyDown(KeyCode.E) && isTalking == true)
            {
                EndDialogue();
            }

            // Set the player response text based on which dialogue you have arrived at //
            /// Add as many player responses as you want (as many if, else if, else if, etc.)
            if (curResponseTracker == 0 && npc.playerDialogue.Length >= 0)
            {
                playerResponse.text = npc.playerDialogue[0];

                // Select that dialogue (press enter)
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = npc.dialogue[1];
                }
            }
            else if (curResponseTracker == 1 && npc.playerDialogue.Length >= 1)
            {
                playerResponse.text = npc.playerDialogue[1];

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = npc.dialogue[2];
                }
            }
            else if (curResponseTracker == 2 && npc.playerDialogue.Length >= 2)
            {
                playerResponse.text = npc.playerDialogue[2];

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = npc.dialogue[3];
                }
            }
        }
    }

    void StartConversation()
    {
        isTalking = true;
        // Start the conversation fresh
        curResponseTracker = 0;
        // Pop up the dialogue box
        dialogueUI.SetActive(true);
        // Set the NPC name
        npcName.text = npc.name;
        // Set the NPC dialogue (greeting message is at [0])
        npcDialogueBox.text = npc.dialogue[0];
    }

    void EndDialogue()
    {
        // Set isTalking back to false
        isTalking = false;
        // Remove dialogue box
        dialogueUI.SetActive(false);
    }
}