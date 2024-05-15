using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Vectors : MonoBehaviour
{
    public GameObject my_object;
    private void DrawVector(Vector3 pos, Vector3 v, Color c)
    {
        Gizmos.color = c;
        Gizmos.DrawLine(pos, pos+v);

        Handles.color = c;
        Handles.ConeHandleCap(0, (pos + v) - v.normalized * 0.35F, Quaternion.LookRotation(v), 0.5f, EventType.Repaint);
    }
    private void OnDrawGizmos()
    {
  
        DrawVector(Vector3.zero, new Vector3(5, 0, 0), Color.red);

        DrawVector(Vector3.zero, new Vector3(0, 5, 0), Color.green);

        DrawVector(Vector3.zero ,my_object.transform.position, Color.magenta);

        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(Vector3.zero, 1);
    }


}
