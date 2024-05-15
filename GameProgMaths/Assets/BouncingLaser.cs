using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BouncingLaser : MonoBehaviour
{
    [Range(1, 100)]
    public int bounces = 10;
    private void OnDrawGizmos()
    {
        RaycastHit hit;
        Vector3 rayDirection = transform.right;
        Vector3 raystart = transform.position;


        for(int i = 0; i < bounces; i++)
        {
            if (Physics.Raycast(raystart, rayDirection, out hit))
            {
                Handles.color = Color.red;
                Handles.DrawLine(raystart, hit.point);
                raystart = hit.point;
                rayDirection = rayDirection - 2 * Vector3.Dot(rayDirection, hit.normal) * hit.normal;
            }
        }

        
            
            

    }
}
