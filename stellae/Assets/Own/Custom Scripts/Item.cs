using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public float fadeDistCLOSE, fadeDistFAR, hitDistCLOSE, hitDistFAR;
    public bool look, inspect, take, assemble;
    public List<string> assembleItems;

    public void assembleFunc()
    {
        GameObject player = GameObject.Find("Player");
        bool hasItems = false;
        for(int i = 0; i < assembleItems.Count;i++)
        {
            hasItems = player.GetComponent<Inventory>().inventory.Contains(assembleItems[i]);
        }
        if (hasItems)
        {
            for (int i = 0; i < assembleItems.Count; i++)
            {
                player.GetComponent<Inventory>().inventory.Remove(assembleItems[i]);
            }
            player.GetComponent<Inventory>().invUpdate();
            GameObject sibling = transform.parent.GetChild(0).gameObject;
            gameObject.SetActive(false);
            sibling.SetActive(true);
            Debug.Log("assembled");
        }
    }
}

