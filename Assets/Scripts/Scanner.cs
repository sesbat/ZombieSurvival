using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targetRays;
    public Transform nearTarget;

    private void FixedUpdate()
    {
        targetRays = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearTarget = GetNear();
    }
    Transform GetNear()
    {
        Transform result = null;
        float dis = 100;

        foreach(var target in targetRays)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float distance = Vector3.Distance(myPos, targetPos);

            if(distance < dis)
            {
                dis = distance;
                result = target.transform;
            }
        }

        return result;
    }
}
