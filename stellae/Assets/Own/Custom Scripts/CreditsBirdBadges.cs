using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsBirdBadges : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("birdinventory").GetComponent<PermaInv>().normal)
        {
            GameObject.Find("birdpatch").GetComponent<Image>().enabled = true;
        }
        if (GameObject.Find("birdinventory").GetComponent<PermaInv>().rare)
        {
            GameObject.Find("rarebirdpatch").GetComponent<Image>().enabled = true;
        }
    }

}
