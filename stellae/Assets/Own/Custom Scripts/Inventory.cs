using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public List<string> inventory;
    public string invDebug;
    public GameObject tempinv;
    public GameObject inv;
    public Dictionary<string,int> maxCarry = new Dictionary<string, int>()
    {
        {"rocks", 3},
        {"stick", 3},
        {"log", 2}
    };

    void Start()
    {
        inventory = new List<string>();
        invDebug = "inventory: ";
        tempinv.GetComponent<Text>().text = "" + invDebug;
        inv = GameObject.Find("inventory");
    }

    public bool invUpdate(string itemName)
    {
        if (maxCarry.ContainsKey(itemName)) //if weight restricted
        {
            if (invCount(itemName) < maxCarry[itemName]) //less than limit
            {
                inventory.Add(itemName);
                invUpdate();
                return true;
            }
            else //hit limit
            {
                GetComponent<Thought>().Think("I think I have enough of these");
                return false;
            }
        }

        inventory.Add(itemName);
        invUpdate();
        return true;
    }

    public void invUpdate()
    {
        //debug inventory
        invDebug = "inventory: ";
        for (int i = 0; i < inventory.Count; i++)
        {
            invDebug += inventory[i] + ", ";
        }
        
        tempinv.GetComponent<Text>().text = "" + invDebug;

        //real inventory
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "2_Freeroam1")
        {
            if (invCount("green tent") == 0)
            {
                GameObject.Find("greentent").GetComponent<Image>().enabled = false;
            }
            if (invCount("green tent") == 1)
            {
                GameObject.Find("greentent").GetComponent<Image>().enabled = true;
            }
            if (invCount("red tent") == 0)
            {
                GameObject.Find("redtent").GetComponent<Image>().enabled = false;
            }
            if (invCount("red tent") == 1)
            {
                GameObject.Find("redtent").GetComponent<Image>().enabled = true;
            }
            if (invCount("red chair") == 0)
            {
                GameObject.Find("redchair").GetComponent<Image>().enabled = false;
            }
            if (invCount("red chair") == 1)
            {
                GameObject.Find("redchair").GetComponent<Image>().enabled = true;
            }
            if (invCount("green chair") == 0)
            {
                GameObject.Find("greenchair").GetComponent<Image>().enabled = false;
            }
            if (invCount("green chair") == 1)
            {
                GameObject.Find("greenchair").GetComponent<Image>().enabled = true;
            }
        }
        if(scene.name == "4_Freeroam2") 
        { 
            if (invCount("stick") == 0)
            {
                GameObject.Find("1stick").GetComponent<Image>().enabled = false;
                GameObject.Find("2stick").GetComponent<Image>().enabled = false;
                GameObject.Find("3stick").GetComponent<Image>().enabled = false;
            }
            if (invCount("stick") == 1)
            {
                GameObject.Find("1stick").GetComponent<Image>().enabled = true;
                GameObject.Find("2stick").GetComponent<Image>().enabled = false;
                GameObject.Find("3stick").GetComponent<Image>().enabled = false;
            }
            if (invCount("stick") == 2)
            {
                GameObject.Find("1stick").GetComponent<Image>().enabled = false;
                GameObject.Find("2stick").GetComponent<Image>().enabled = true;
                GameObject.Find("3stick").GetComponent<Image>().enabled = false;
            }
            if (invCount("stick") == 3)
            {
                GameObject.Find("1stick").GetComponent<Image>().enabled = false;
                GameObject.Find("2stick").GetComponent<Image>().enabled = false;
                GameObject.Find("3stick").GetComponent<Image>().enabled = true;
            }
            if (invCount("rocks") == 0)
            {
                GameObject.Find("1rocks").GetComponent<Image>().enabled = false;
                GameObject.Find("2rocks").GetComponent<Image>().enabled = false;
                GameObject.Find("3rocks").GetComponent<Image>().enabled = false;
            }
            if (invCount("rocks") == 1)
            {
                GameObject.Find("1rocks").GetComponent<Image>().enabled = true;
                GameObject.Find("2rocks").GetComponent<Image>().enabled = false;
                GameObject.Find("3rocks").GetComponent<Image>().enabled = false;
            }
            if (invCount("rocks") == 2)
            {
                GameObject.Find("1rocks").GetComponent<Image>().enabled = false;
                GameObject.Find("2rocks").GetComponent<Image>().enabled = true;
                GameObject.Find("3rocks").GetComponent<Image>().enabled = false;
            }
            if (invCount("rocks") == 3)
            {
                GameObject.Find("1rocks").GetComponent<Image>().enabled = false;
                GameObject.Find("2rocks").GetComponent<Image>().enabled = false;
                GameObject.Find("3rocks").GetComponent<Image>().enabled = true;
            }
            if (invCount("log") == 0)
            {
                GameObject.Find("1log").GetComponent<Image>().enabled = false;
                GameObject.Find("2log").GetComponent<Image>().enabled = false;
            }
            if (invCount("log") == 1)
            {
                GameObject.Find("1log").GetComponent<Image>().enabled = true;
                GameObject.Find("2log").GetComponent<Image>().enabled = false;
            }
            if (invCount("log") == 2)
            {
                GameObject.Find("1log").GetComponent<Image>().enabled = false;
                GameObject.Find("2log").GetComponent<Image>().enabled = true;
            }
            if (invCount("lighter") == 0)
            {
                GameObject.Find("lighter").GetComponent<Image>().enabled = false;
            }
            if (invCount("lighter") == 1)
            {
                GameObject.Find("lighter").GetComponent<Image>().enabled = true;
            }
        }
        if(scene.name == "6_Freeroam3")
        {
            if (invCount("sleeping bag") == 0)
            {
                GameObject.Find("sleepingbagred").GetComponent<Image>().enabled = false;
            }
            if (invCount("sleeping bag") == 1)
            {
                GameObject.Find("sleepingbagred").GetComponent<Image>().enabled = true;
            }
        }
    }

    public int invCount(string itemName)
    {
        int temp = 0;
        for(int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == itemName)
            {
                temp++;
            }
        }
        return temp;
    }
}