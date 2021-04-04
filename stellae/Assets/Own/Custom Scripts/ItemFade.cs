using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFade : MonoBehaviour
{
    Camera cameraToLookAt;
    Material[] ok;
    Item item;
    public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
        return (NewValue);
    }

    // Use this for initialization 
    void Start()
    {
        cameraToLookAt = Camera.main;
        ok = GetComponent<Renderer>().materials;
        item = GetComponent<Item>();
    }

    // Update is called once per frame 
    void LateUpdate()
    {
        float Dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        if (Dist > item.fadeDistFAR*2)
        {
            foreach (Material element in ok)
            {
                element.color= new Color(0.3f, 0.7f, 0.7f, 0f);
            }
        }
        if (Dist < item.fadeDistFAR*2 && Dist > item.fadeDistCLOSE)
        {
            float temp = scale(item.fadeDistCLOSE, item.fadeDistFAR*2, 0.4f, 0, Dist);

            foreach (Material element in ok)
            {
                element.color = new Color(0.3f, 0.7f, 0.7f, temp);
            }
        }
        if(Dist < 2)
        {
            foreach (Material element in ok)
            {
                element.color = new Color(0.3f, 0.7f, 0.7f, 0.4f);
            }
        }
    }
}