using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<string> inventory;
    public string invDebug;
    public GameObject tempinv;
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
        invDebug = "inventory: ";
        for (int i = 0; i < inventory.Count; i++)
        {
            invDebug += inventory[i] + ", ";
        }
        
        tempinv.GetComponent<Text>().text = "" + invDebug;
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