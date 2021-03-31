using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Ink.Runtime;

public class InkDialogue : MonoBehaviour
{

    private Story story;
    public TextAsset inkJSONAsset;
    public Button buttonPrefab;
    bool isTalking = false;
    bool storyLoaded = false;
    bool previouslyLoadedStory = false;
    string text = "";



    // === Create an event for a keyPress to make dialogue happen! (IMPORTANT) === //
    UnityEvent myEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        // 1. The ink file will be loaded as a new Story object //
        story = new Story(inkJSONAsset.text);

        // 2. Add a listener to the event -- event sends to --> makeDialogueSelection (IMPORTANT)
        myEvent.AddListener(makeDialogueSelection);

        // 3. Start the refresh cycle //
        Debug.Log("start refresh");
        refresh();
    }




    // Refresh the UI (called once per frame)
    void refresh()
    {
        // =============== TRIGGERING DIALOGUE (COLLISION TRIGGER) =============== //
        GameObject x = GameObject.Find("d_trigger");
        varController var = x.GetComponent<varController>();

        // -- Triggering dialogue -- //
        if (var.dialogueTriggered == true)
        {
            isTalking = true;
        }

        // =============== START CONVERSATION (F) =============== //
        if (Input.GetKeyDown(KeyCode.F) && isTalking == false)
        {
            Debug.Log("starting convo");
            isTalking = true;
        }

        // =============== END CONVERSATION (X) =============== //
        if (Input.GetKeyDown(KeyCode.X) && isTalking == true)
        {
            Debug.Log("ending convo");
            isTalking = false;
            storyLoaded = false;
            clearUI();
        }

        // =============== 3. LOADING STORY (if necessary) =============== //
        if (isTalking == true && storyLoaded == false)
        {
            // Clear the UI
            clearUI();

            // Create a new GameObject
            GameObject newGameObject = new GameObject("TextChunk");
            // Set its transform to the Canvas (this)
            newGameObject.transform.SetParent(this.transform, false);

            // Add a new Text component to the new GameObject
            Text newTextObject = newGameObject.AddComponent<Text>();
            // Set the fontSize larger
            newTextObject.fontSize = 24;
            // Set the text from new story block

            if (previouslyLoadedStory == false)
            {
                text = continueStory();
                Debug.Log("continuing story");
                previouslyLoadedStory = true;
            }

            /* ========== TAGS ========== */


            // Get the current tags (if any)
            List<string> tags = story.currentTags;

            // If there are tags, use the first one.
            // Otherwise, just show the text.
            if (tags.Count > 0)
            {
                newTextObject.text = "<color=grey>" + tags[0] + "</color> – " + text;
            }
            else
            {
                newTextObject.text = text;
            }



            // Load Arial from the built-in resources
            newTextObject.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

            // Create buttons //
            foreach (Choice choice in story.currentChoices)
            {
                Button choiceButton = Instantiate(buttonPrefab) as Button;
                choiceButton.transform.SetParent(this.transform, false);

                // Gets the text from the button prefab
                Text choiceText = choiceButton.GetComponentInChildren<Text>();
                choiceText.text = " " + (choice.index + 1) + ". " + choice.text;
            }
            storyLoaded = true;
        }
    }


    // Clear out all of the UI, calling Destroy() in reverse
    void clearUI()
    {
        int childCount = this.transform.childCount;
        // Changing i >= ___ to be the # of children we currently have -- this will prevent other HUD elements from being deleted
        for (int i = childCount - 1; i >= 2; i--)
        {
            GameObject.Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Listener (input during convo) -- this is why the choices are pressed!!! //
        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)) && isTalking == true)
        {
            // Go to makeDialogueSelection //
            myEvent.Invoke();
        }
        
        // Refresh
        refresh();
    }

    // Verify existence and load the next part of the story //
    string continueStory()
    {
        string text = "";

        // Check that story can continue //
        if (story.canContinue)
        {
            // Will continue the story until the next set of choices is found //
            text = story.ContinueMaximally();
        }

        return text;
    }


    // ========== NUMBER KEY PRESS TRACKER ========== //
    int getNumberKeyPressed()
    {
        // 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 1;
        }
        // 2
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 2;
        }
        // 3
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 3;
        }
        // 4
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 4;
        }
        // No number key pressed
        else
        {
            return -1;
        }
    }

    // NEW VERSION OF DIALOGUE SELECTION //
    void makeDialogueSelection()
    {

        // 1. Call getNumberPressed() //
        int number = getNumberKeyPressed();
        Debug.Log("Number is: ");
        Debug.Log(number);


        // 2. Check that number generated is a valid choice number //
        int numberOfChoices = story.currentChoices.Count;
        if (number <= numberOfChoices && number != -1)
        {

            // 3. If it is... make the choice //
            Debug.Log("Choice made");
            // NOTE: 1st choice = index 0 ------ 2nd choice = index 1 ----- etc. (for any given set of dialogue)
            story.ChooseChoiceIndex(number - 1);


            // 4. Reset variables //
            storyLoaded = false;
            previouslyLoadedStory = false;

        }
    }
}

/* Credit to Dan Cox for some sections of code used in:

    * Section 3 (LOADING STORY) of refresh() function
    * clearUI() functionality
     
*/
