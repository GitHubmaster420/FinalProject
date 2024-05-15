using System;
using UnityEditor;
using UnityEngine;

public class RadialTrigger : MonoBehaviour
{
    public GameObject Player;
    public GameObject LookAt;
    public float radius = 5.7f;
    public float angle = 45f;
    public float heightTrigger = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Vector3 npcToPlayer = Player.transform.position - gameObject.transform.position;
        Vector3 npcToLookAt = LookAt.transform.position - gameObject.transform.position;
        if (npcToPlayer.magnitude > radius || Vector3.Dot(new Vector3(npcToLookAt.x, 0, npcToLookAt.z).normalized, new Vector3(npcToPlayer.x, 0, npcToPlayer.z).normalized) < Math.Cos(angle * Mathf.Deg2Rad) || Math.Abs(npcToPlayer.y) > heightTrigger)
        {
            Handles.color = Color.green;
        }
        else
        {
            Handles.color = Color.red;
        }

        Handles.DrawWireDisc(gameObject.transform.position + heightTrigger * Vector3.up, new Vector3(0, 1, 0), radius);
        Handles.DrawWireDisc(gameObject.transform.position - heightTrigger * Vector3.up, new Vector3(0, 1, 0), radius);


        if (Vector3.Dot(npcToLookAt.normalized, npcToPlayer.normalized) > Math.Cos(angle * Mathf.Deg2Rad))
        {
            Handles.color = Color.red;
        }
        else
        {
            Handles.color = Color.green;
        }
        DrawVector(gameObject.transform.position, npcToPlayer, Handles.color);
        DrawVector(gameObject.transform.position, npcToLookAt, Color.magenta);

        Vector3 npcToLookAtFlatNormalized = new Vector3(npcToLookAt.x, gameObject.transform.position.y, npcToLookAt.z).normalized;
        Vector3 npcTLAFNRotatedRight = Quaternion.AngleAxis(angle, Vector3.up) * npcToLookAtFlatNormalized * radius;
        Vector3 npcTLAFNRotatedLeft = Quaternion.AngleAxis(-angle, Vector3.up) * npcToLookAtFlatNormalized * radius;
        DrawVector(gameObject.transform.position + Vector3.up * heightTrigger, npcTLAFNRotatedRight, Handles.color);
        DrawVector(gameObject.transform.position + Vector3.up * heightTrigger, npcTLAFNRotatedLeft, Handles.color);
        DrawVector(gameObject.transform.position - Vector3.up * heightTrigger, npcTLAFNRotatedRight, Handles.color);
        DrawVector(gameObject.transform.position - Vector3.up * heightTrigger, npcTLAFNRotatedLeft, Handles.color);
        DrawVector(gameObject.transform.position + npcTLAFNRotatedRight - Vector3.up * heightTrigger, Vector3.up * heightTrigger * 2, Handles.color);
        DrawVector(gameObject.transform.position + npcTLAFNRotatedLeft - Vector3.up * heightTrigger, Vector3.up * heightTrigger * 2, Handles.color);

    }

    private void DrawVector(Vector3 pos, Vector3 v, Color c, float thickness = 0.0f)
    {
        //Gizmos.color = c;
        //Gizmos.DrawLine(pos, pos + v);
        // Arrow head?
        Handles.color = c;
        Handles.DrawLine(pos, pos + v, thickness);

        // Compute the "rough" endpoint for the cone
        // Normalize the vector (its magnitude becomes 1)
        Vector3 n = v.normalized;
        n = n * 0.35f; // Now the length is 35cm

        Handles.ConeHandleCap(0, pos + v - n, Quaternion.LookRotation(v), 0.5f, EventType.Repaint);

    }

}
