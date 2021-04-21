using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermaInv : MonoBehaviour
{
    public bool crow, cardinal, bluejay, hummingbird, tanager, lark, normal, rare;
    //GameObject journal;
    public string text;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        text = "Birds Spotted:";
    }
    public void updateJournal()
    {
        if(crow && cardinal && bluejay)
        {
            normal = true;
        }
        if (hummingbird && tanager && lark)
        {
            rare = true;
        }
        string tempA = "Birds Spotted:\r\n";
        if (crow)
        {
            tempA += "Crow\r\n";
        }
        if (cardinal)
        {
            tempA += "Cardinal\r\n";
        }
        if (bluejay)
        {
            tempA += "Bluejay\r\n";
        }
        if (hummingbird)
        {
            tempA += "Hummingbird (rare)\r\n";
        }
        if (tanager)
        {
            tempA += "Tanager (rare)\r\n";
        }
        if (lark)
        {
            tempA += "Lark (rare)\r\n";
        }
        text = tempA;
    }
}
