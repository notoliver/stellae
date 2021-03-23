using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float baseFOV = 80;
    public float zoomFOV = 30;
    public float t = 0.2f;

    void Update()
    {
        interaction();

        zoom();
        
        if (Input.GetKeyDown(KeyCode.R)) //R debug
        {
            for (int i = 0; i < GetComponent<Inventory>().inventory.Count; i++)
            {
                Debug.Log(GetComponent<Inventory>().inventory[i]);
            }          
        }
    }

    void interaction()
    {
        if (Input.GetMouseButtonUp(0)) //lmb
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<Item>() != null)
                {
                    float Dist = Vector3.Distance(Camera.main.transform.position, hitObject.transform.position);
                    Item selected = hitObject.GetComponent<Item>();

                    if (Dist > selected.hitDistCLOSE && Dist < selected.hitDistFAR)
                    {
                        if (selected.take)
                        {
                            hitObject.SetActive(false);
                            Debug.Log("take");
                            GetComponent<Inventory>().invUpdate(selected.itemName);
                        }
                        else if (selected.look)
                        {
                            if (!GetComponent<Inventory>().inventory.Contains(selected.itemName))
                            {
                                GetComponent<Inventory>().invUpdate(selected.itemName);
                                Debug.Log("look");
                            }
                        }
                        else if (selected.inspect)
                        {
                            Debug.Log("inspect");
                        }
                        else if (selected.assemble)
                        {
                            selected.assembleFunc();                          
                        }
                    }
                }
            }
        }
    }
    void zoom()
    {
        if (Input.GetMouseButton(1)) //rmb
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomFOV, t);
        else
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, baseFOV, t);
    }
}
