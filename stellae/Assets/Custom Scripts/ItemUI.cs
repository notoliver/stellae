using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    Camera cameraToLookAt;
    TextMesh textmesh;
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
        textmesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame 
    void LateUpdate()
    {
        float Dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        if (Dist > 10)
        {
            textmesh.color = new Color(0, 0, 0, 0);
        }
        if (Dist < 10 && Dist > 2)
        {
            float temp = scale(2, 10, 1, 0, Dist);
            textmesh.color = new Color(1, 1, 1, temp);
        }
        if(Dist < 2)
        {
            textmesh.color = new Color(1, 1, 1, 1);
        }
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}

