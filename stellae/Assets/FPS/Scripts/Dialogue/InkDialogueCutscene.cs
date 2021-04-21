using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Ink.Runtime;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;
using System.Threading;
using System;
using UnityEngine.SceneManagement;

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
    int num = 0;
    bool show_Buttons = false;
    bool setThought = false;
    bool stopWatch_started = false;
    // ===== Use Stopwatch to delay time ===== //
    Stopwatch stopWatch = new Stopwatch();

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
        UnityEngine.Debug.Log("start refresh");
        refresh();
    }





    // Refresh the UI (called once per frame)
    void refresh()
    {
       
        // =============== 1. LOADING STORY =============== //
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
                UnityEngine.Debug.Log("continuing story");
                previouslyLoadedStory = true;
            }

            /* ========== TAGS ========== */


            // Get the current tags (if any)
            List<string> tags = story.currentTags;

            // If there are tags, use the first one. Otherwise, just show the text.
            if (tags.Count > 0)
            {
                newTextObject.text = "<color=grey>" + tags[0] + "</color> – " + text;
            }
            else
            {
                newTextObject.text = text;
                StartCoroutine(FadeTextToFullAlpha(1f, newTextObject));
            }



            // Load Arial from the built-in resources
            newTextObject.font = myFont;

            // Create buttons //
            foreach (Choice choice in story.currentChoices)
            {
                if (show_Buttons == true)
                {
                    Button choiceButton = Instantiate(buttonPrefab) as Button;
                    choiceButton.transform.SetParent(t.transform, false);

                    // Gets the text from the button prefab
                    Text choiceText = choiceButton.GetComponentInChildren<Text>();
                    choiceText.text = " " + (choice.index + 1) + ". " + choice.text;
                }
            }
            storyLoaded = true;
            show_Buttons = false;
        }
    }






    // ============= Clear out all of the UI, calling Destroy() in reverse ============= //
    void clearUI()
    {
        // === 1. Destroy HUD children === //
        int childCount = this.transform.childCount;
        // Changing i >= ___ to be the # of children we currently have -- this will prevent other HUD elements from being deleted
        for (int i = childCount - 1; i >= 5; i--)
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

        if (num < 10)
        {
            num++;
        }
        else if (num == 10)
        {
            show_Buttons = true;
            num = 0;
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
        if(text.IndexOf("actually") > -1 || text.IndexOf("bite") > -1)
        {
            GameObject.Find("fishing").GetComponent<Animator>().SetTrigger("castingTrigger");
        }
        if (text.IndexOf("Smores") > -1 || text.IndexOf("remember is meeting") > -1)
        {
            GameObject.Find("campfireRoasting").GetComponent<Animator>().SetTrigger("sitForward");
        }
        if (text.IndexOf("big dipper") > -1 || text.IndexOf("galloping") > -1)
        {
            GameObject.Find("laying_down").GetComponent<Animator>().SetTrigger("point");
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






    // ============= DIALOGUE SELECTION ============= //
    void makeDialogueSelection()
    {

        // 1. Call getNumberPressed() //
        int number = getNumberKeyPressed();
        UnityEngine.Debug.Log("Number is: ");
        UnityEngine.Debug.Log(number);


        // 2. Check that number generated is a valid choice number //
        int numberOfChoices = story.currentChoices.Count;
        if (number <= numberOfChoices && number != -1)
        {

            // 3. If it is... make the choice //
            UnityEngine.Debug.Log("Choice made");
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
            // Start the stopwatch (delays scene transition) //
            if (stopWatch_started == false)
            {
                UnityEngine.Debug.Log("Starting stopwatch");
                stopWatch.Start();
                stopWatch_started = true;
            }

            Scene scene = SceneManager.GetActiveScene();
            int ms = 5000;
            if(scene.name == "7_Stargazing")
            {
                ms = 10000;
            }
            if (stopWatch.ElapsedMilliseconds > ms && setThought == false)
            {
                GameObject.Find("Player").GetComponent<Thought>().Think("Press Space to continue to the next scene.");
                setThought = true;
            }

            // Add a delay //
            if (Input.GetKeyDown(KeyCode.Space) && stopWatch.ElapsedMilliseconds > 5000)
            {
                UnityEngine.Debug.Log(stopWatch.ElapsedMilliseconds);
                endObject.GetComponent<LoadLevelScript>().ApplicationLoadLevel();
            }
        }
    }




    /* ===== TIME DELAY FUNCTION (FAILED) ===== */
    IEnumerator timeDelay()
    {
        UnityEngine.Debug.Log("Started Coroutine at :" + Time.time);
        yield return new WaitForSeconds(5);
        delay_time = delay_time + 1;
        UnityEngine.Debug.Log("Ended at: " + Time.time);
    }






    // ========== TEXT FADE ========== //
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}

/* Credit to Dan Cox for some sections of code used in:

    * Section 4 (LOADING STORY) of refresh() function
    * Part of clearUI() functionality
     
*/
