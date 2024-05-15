using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidFinger : MonoBehaviour
{
    public Transform player;
    public float angle = 45;
    public GameObject midFinger;
    public GameObject notMidFinger;
    void Update()
    {
        Vector3 forward = transform.forward;
        Vector3 npcToPlayerNormalized = (player.position- transform.position).normalized;
        if (Vector3.Dot(forward, npcToPlayerNormalized) > Math.Cos(angle * Mathf.Deg2Rad))
        {
            midFinger.SetActive(true);
            notMidFinger.SetActive(false);
        }
        else
        {
            midFinger.SetActive(false);
            notMidFinger.SetActive(true);
        }
    }
}
