using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Item>().take = false;
            child.transform.GetChild(0).gameObject.SetActive(false);
            //Debug.Log(child.GetComponent<Item>().itemName);
        }
    }
}
