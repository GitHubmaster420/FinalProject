using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StreetLight : MonoBehaviour
{
    public Light spotLight;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if((gameObject.transform.position - player.transform.position).magnitude > 30)
        {
            spotLight.intensity = 0;
        }
        else
        {
            spotLight.intensity = 20;
        }
    }
}
