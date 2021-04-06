using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Ink.Runtime;
using System.Threading.Tasks;
using System.Timers;

public class InkDialogueCutscene : MonoBehaviour
{
    // ============= VARIABLES ============= //
    private Story story;
    public TextAsset inkJSONAsset;
    public Button buttonPrefab;
    bool isTalking = true;
    bool storyLoaded = false;
    bool previouslyLoadedStory = false;
    string text = "";
    int delay_time = 0;
    bool started_delay = false;
    public GameObject endObject;
    public Font myFont;


    // === Create an event for a keyPress to make dialogue choices happen! (IMPORTANT) === //
    UnityEvent myEvent = new UnityEvent();
    // == Event to advance to next line of dialogue using ENTER key (no choices) == //
    UnityEvent enter_Event = new UnityEvent();





    // Start is called before the first frame update
    void Start()
    {
        // 1. The ink file will be loaded as a new Story object //
        story = new Story(inkJSONAsset.text);

        // 2. Add a listener to the events
        // myEvent sends to --> makeDialogueSelection (IMPORTANT)
        myEvent.AddListener(makeDialogueSelection);
        enter_Event.AddListener(nextLineOfDialogue);

        // 3. Start the refresh cycle //
        Debug.Log("start refresh");
        refresh();
    }





    // Refresh the UI (called once per frame)
    void refresh()
    {
       
        // =============== 4. LOADING STORY (if necessary) =============== //
        if (isTalking == true && storyLoaded == false)
        {
            // Clear the UI
            clearUI();

            // Create a new GameObject
            GameObject newGameObject = new GameObject("TextChunk");

            // Get transform of Dialogue (child of HUD)
            GameObject dialogue = GameObject.Find("Dialogue");
            Transform t = dialogue.GetComponent<Transform>();

            // Set its transform to the Dialogue child (this.t)
            newGameObject.transform.SetParent(t.transform, false);
            
            // Add a new Text component to the new GameObject (like what we did with dialogueTriggered)
            Text newTextObject = newGameObject.AddComponent<Text>();

            // Set the fontSize larger
            newTextObject.fontSize = 40;

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
            newTextObject.font = myFont;

            // Create buttons //
            foreach (Choice choice in story.currentChoices)
            {
                Button choiceButton = Instantiate(buttonPrefab) as Button;
                choiceButton.transform.SetParent(t.transform, false);

                // Gets the text from the button prefab
                Text choiceText = choiceButton.GetComponentInChildren<Text>();
                choiceText.text = " " + (choice.index + 1) + ". " + choice.text;
            }
            storyLoaded = true;
        }
    }






    // ============= Clear out all of the UI, calling Destroy() in reverse ============= //
    void clearUI()
    {
        // === 1. Destroy HUD children === //
        int childCount = this.transform.childCount;
        // Changing i >= ___ to be the # of children we currently have -- this will prevent other HUD elements from being deleted
        for (int i = childCount - 1; i >= 4; i--)
        {
            GameObject.Destroy(this.transform.GetChild(i).gameObject);
        }
        // === 2. Get transform of Dialogue (child of HUD) === //
        GameObject dialogue = GameObject.Find("Dialogue");
        Transform t = dialogue.GetComponent<Transform>();

        // === 3. Destroy children of Dialogue child === //
        int dialogue_child_count = t.transform.childCount;

        for (int i = dialogue_child_count - 1; i >= 0; i--)
        {
            GameObject.Destroy(t.transform.GetChild(i).gameObject);
        }
    }





    // ============= UPDATE ============= //
    void Update()
    {
        // Listener (input during convo) -- this is why the choices are pressed!!! //
        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)) && isTalking == true)
        {
            // Go to makeDialogueSelection //
            myEvent.Invoke();
        }
        else if ((Input.GetKeyDown(KeyCode.Return))||Input.GetKeyDown(KeyCode.Space))
        {
            // Go to continueStory()
            enter_Event.Invoke();
        }
        
        // Refresh
        refresh();
        checkEnd();
    }





    // ============= Verify existence and load the next part of the story ============= //
    string continueStory()
    {
        string text = "";

        // Check that story can continue //
        if (story.canContinue)
        {
            // Will continue the story until the next set of choices is found //
            text = story.Continue();
        }

        return text;
    }





    // ============= Meant for advancing the story when NO CHOICES are present ============= //
    void nextLineOfDialogue()
    {
        int numberOfChoices = story.currentChoices.Count;

        // Reset variables //
        if (numberOfChoices == 0)
        {
            storyLoaded = false;
            previouslyLoadedStory = false;
        }
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






    // ============= NEW VERSION OF DIALOGUE SELECTION ============= //
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


    void checkEnd()
    {
        if (!story.canContinue)
        {
            endObject.GetComponent<LoadLevelScript>().ApplicationLoadLevel();
        }
    }

    /* ===== TIME DELAY FUNCTION (FAILED) ===== */
    IEnumerator timeDelay()
    {
        Debug.Log("Started Coroutine at :" + Time.time);
        yield return new WaitForSeconds(5);
        delay_time = delay_time + 1;
        Debug.Log("Ended at: " + Time.time);
    }
}

/* Credit to Dan Cox for some sections of code used in:

    * Section 4 (LOADING STORY) of refresh() function
    * Part of clearUI() functionality
     
*/
