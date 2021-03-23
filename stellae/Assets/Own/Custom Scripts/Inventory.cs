using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<string> inventory;
    public string invDebug;
    public GameObject tempinv;

    void Start()
    {
        inventory = new List<string>();
        tempinv = GameObject.Find("tempINV");
        invDebug = "inventory: ";
        tempinv.GetComponent<Text>().text = "" + invDebug;
    }

    public void invUpdate(string itemName)
    {
        if (!inventory.Contains(itemName))
        {
            inventory.Add(itemName);
        }
        invUpdate();
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
}