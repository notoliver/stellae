using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thought : MonoBehaviour
{
    public GameObject thoughts;
    Text box;
    public string lastThought;
    public bool thinking;

    void Start()
    {
        box = thoughts.GetComponent<Text>();
        box.text = "";
        thinking = false;
    }

    void Update() //debug
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Think(lastThought);
        }
    }

    public void Think(string thought)
    {
        lastThought = thought;
        StartCoroutine(ThinkCoroutine(thought));
    }

    IEnumerator ThinkCoroutine(string thought)
    {
        thoughts.GetComponent<Text>().text = thought;
        StartCoroutine(FadeTextToFullAlpha(0.3f, box));
        if (!thinking)
        {
            thinking = true;
            yield return new WaitForSeconds(7);
            thinking = false;
            StartCoroutine(FadeTextToZeroAlpha(0.3f, box));
        }
        /*else
        {
            yield return 0;
        }*/
    }

    IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
