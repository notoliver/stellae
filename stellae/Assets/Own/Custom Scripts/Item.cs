using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public float fadeDistCLOSE, fadeDistFAR, hitDistCLOSE, hitDistFAR;
    public bool look, inspect, take, assemble, assembleUnique;
    public List<string> assembleItems;
    public List<int> assembleItemsNums;
    public List<string> thoughts;

    public void assembleFunc()
    {
        if (assemble)
        {
            GameObject player = GameObject.Find("Player");
            bool hasItems = false;
            for (int i = 0; i < assembleItems.Count; i++)
            {
                if (player.GetComponent<Inventory>().invCount(assembleItems[i]) != assembleItemsNums[i])
                {
                    player.GetComponent<Thought>().Think("I don't have what I need"); //thought
                    hasItems = false;
                    break;
                }
                else
                {
                    hasItems = true;
                }
            }
            if (hasItems)
            {
                for (int i = 0; i < assembleItems.Count; i++)
                {
                    for(int times = 0; times < assembleItemsNums[i]; times++)
                    {
                        for (int j = 0; j < player.GetComponent<Inventory>().inventory.Count; j++)
                        {
                            player.GetComponent<Inventory>().inventory.Remove(assembleItems[i]);
                        }
                    }  
                }
                player.GetComponent<Inventory>().invUpdate();
                GameObject sibling = transform.parent.GetChild(0).gameObject;
                gameObject.SetActive(false);
                sibling.SetActive(true);
                if(thoughts.Count != 0)
                {
                    player.GetComponent<Thought>().Think(thoughts[0]); //thought
                }     
            }
        }
        
    }
}

