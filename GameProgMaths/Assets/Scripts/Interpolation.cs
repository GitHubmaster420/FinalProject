using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interpolation : MonoBehaviour
{
    public GameObject goA;
    public GameObject goB;
    public GameObject Player;
    public float Interp_time = 5.0f;

    [Range(0f, 10f)]
    public float elapsedTime = 0f;
    private void DrawVector(Vector3 pos, Vector3 v, Color c)
    {
        Gizmos.color = c;
        Gizmos.DrawLine(pos, pos + v);

        Handles.color = c;
        Handles.ConeHandleCap(0, (pos + v) - v.normalized * 0.35F, Quaternion.LookRotation(v), 0.5f, EventType.Repaint);
    }

    private void OnDrawGizmos()
    {
        DrawVector(Vector3.zero, goA.transform.position, Color.green);
        DrawVector(Vector3.zero, goB.transform.position, Color.red);

        float t = elapsedTime / Interp_time;

        if (t > 1.0f)
            t = 1.0f;

        //Compute the interpolation
        // f(t) = A * (1-t) + B * t

        Vector3 pos = (1 - t) * goA.transform.position + t * goB.transform.position;

        Player.transform.position = pos;

        DrawVectorParts(t);
    }
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f;
    }

    void DrawVectorParts(float t)
    {
        Vector3 partOfA = (1 - t) * goA.transform.position;
        Vector3 partOfB = t * goB.transform.position;

        //Draw the vector
        DrawVector(Vector3.zero, partOfA, Color.magenta);
        DrawVector(partOfA, partOfB, Color.magenta);
    }

    // Update is called once per frame
    void Update()
    {
        //Let's get the elapsed time
        elapsedTime += Time.deltaTime;
        
        // Interpolate until interp_time
        float t = elapsedTime / Interp_time;
        
        if (t > 1.0f)
            t = 1.0f;

        // Easing???
        if (t < 0.5f)
        {
            //t = 2 * t * t; // y = 2*x^2
            t = 4 * t * t * t; // y = 2*x^2
        }
        else
        {
            t = 1 - 2 * (1 - t) * (1 - t); // y = 1 - 2*(1-x)^2
            t = 1 - 4 * (1 - t) * (1 - t) * (1 - t); // y = 1 - 2*(1-x)^2
        }

        //Compute the interpolation
        // f(t) = A * (1-t) + B * t

        Vector3 pos = (1-t) * goA.transform.position + t * goB.transform.position;

        Player.transform.position = pos;
    }
}
