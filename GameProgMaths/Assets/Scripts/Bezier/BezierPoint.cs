using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierPoint : MonoBehaviour
{
    [SerializeField] Transform anchor;
    [SerializeField] Transform[] controls = new Transform[2];

    public Vector3 getAnchorPoint() => anchor.position;
    public Vector3 getFirstControlPoint() => controls[0].position;

    public Vector3 getSecondControlpoint() => controls[1].position;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(getFirstControlPoint(), getAnchorPoint());
        Gizmos.DrawLine(getAnchorPoint(), getSecondControlpoint());
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(getFirstControlPoint(), (float)(HandleUtility.GetHandleSize(getFirstControlPoint()) * 0.1));
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(getAnchorPoint(), 0.1f * HandleUtility.GetHandleSize(getAnchorPoint()));
        Gizmos.DrawSphere(getSecondControlpoint(), 0.1f * HandleUtility.GetHandleSize(getSecondControlpoint()));


    }
}
