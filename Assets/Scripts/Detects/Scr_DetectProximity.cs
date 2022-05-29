using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DetectProximity : Scr_Detect
{
    public float radius = 10.0f;

    protected override void UpdateDetection()
    {
        detectedTarget = null;

        Collider[] colls = Physics.OverlapSphere(transform.position, radius);
        foreach(var coll in colls)
        {
            if(coll.gameObject.tag == targetTag)
            {
                detectedTarget = coll.transform;
                break;
            }
        }
    }

    public Transform IsTargetInVicinity(float newRadius = -1.0f, string newTargetTag = "")
    {
        if (newRadius <= 0.0f) newRadius = 0.0f;
        if (newTargetTag == "") newTargetTag = targetTag;

        Collider[] colls = Physics.OverlapSphere(transform.position, newRadius);
        foreach (var coll in colls)
        {
            if (coll.gameObject.tag == newTargetTag)
            {
                return coll.transform;
            }
        }
        return null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
