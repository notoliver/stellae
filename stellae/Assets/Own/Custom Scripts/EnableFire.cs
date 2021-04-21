using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFire : MonoBehaviour
{
    public GameObject enable;

    void OnEnable()
    {
        enable.SetActive(true);
    }
}
