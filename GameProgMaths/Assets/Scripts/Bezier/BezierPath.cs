using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierPath : MonoBehaviour
{
    public BezierPoint[] points;

    public bool ClosedPath = false;

    private void OnDrawGizmos()
    {
        int n  = points.Length;
        for (int i = 0; i < n-1; i++)
        {
            Vector3 first_anchor = points[i].getAnchorPoint();
            Vector3 second_anchor = points[i+1].getAnchorPoint();

            Vector3 first_control = points[i].getSecondControlpoint();
            Vector3 second_control = points[i + 1].getFirstControlPoint();

            Handles.DrawBezier(first_anchor, second_anchor, first_control, second_control, Color.green, new Texture2D(1, 1), 1);
        }
    }
}
