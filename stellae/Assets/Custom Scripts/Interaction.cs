using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5))
            {

                Debug.Log(hit.transform.tag);
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.tag == "interactive")
                {                   
                    Debug.Log(hitObject.GetComponent<Item>().itemName);
                    hitObject.SetActive(false);
                    GetComponent<Inventory>().inventory.Add(hitObject.GetComponent<Item>().itemName);
                }
            }
        }

        //debug
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < GetComponent<Inventory>().inventory.Count; i++)
            {
                Debug.Log(GetComponent<Inventory>().inventory[i]);
            }          
        }
    }
}
