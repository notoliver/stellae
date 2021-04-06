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
                        //============= TEMPORARY ===============
                        if(selected.itemName == "marvinwake")
                        {
                            GameObject.Find("wakingscript").GetComponent<LoadLevelScript>().ApplicationLoadLevel();
                        }
                        //============= TEMPORARY ===============
                        if (selected.take)
                        {
                            bool added = GetComponent<Inventory>().invUpdate(selected.itemName);
                            if (added)
                            {
                                if(selected.thoughts.Count != 0) { 
                                    int rand = Random.Range(0, selected.thoughts.Count); 
                                    GetComponent<Thought>().Think(selected.thoughts[rand]); //thought
                                }
                                hitObject.SetActive(false);
                            }
                        }
                        else if (selected.look)
                        {
                            if (!GetComponent<Inventory>().inventory.Contains(selected.itemName))
                            {
                                if (selected.assembleUnique) //keep object, disable text
                                {
                                    hitObject.transform.GetChild(0).gameObject.SetActive(false);
                                }
                                if (selected.thoughts.Count != 0) //thought
                                {
                                    int rand = Random.Range(0, selected.thoughts.Count);
                                    Debug.Log(rand);
                                    GetComponent<Thought>().Think(selected.thoughts[rand]);
                                }
                                GetComponent<Inventory>().invUpdate(selected.itemName);
                            }
                        }
                        else if (selected.inspect)
                        {
                            Debug.Log("inspect");
                            int rand = Random.Range(0, selected.thoughts.Count);
                            GetComponent<Thought>().Think(selected.thoughts[rand]); //thought
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
