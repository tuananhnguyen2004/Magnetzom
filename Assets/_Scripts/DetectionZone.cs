using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public float zoneRadius;
    public float minDistance;
    [SerializeField] private LayerMask targetLayer;

    public RaycastHit2D DetectTarget()
    {
        return Physics2D.CircleCast(transform.position, zoneRadius, transform.right, 0f, targetLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, zoneRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }
}
