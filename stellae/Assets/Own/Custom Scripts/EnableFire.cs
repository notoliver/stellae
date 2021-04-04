using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFire : MonoBehaviour
{
    public GameObject enable;

    void OnEnable()
    {
        Debug.Log("enabled");
        enable.SetActive(true);
    }
}
