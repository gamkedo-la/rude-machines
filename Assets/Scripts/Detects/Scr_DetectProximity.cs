using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Scr_DetectProximity : Scr_Detect
{
    public float radius = 10.0f;
    private List<Collider> detectedPlayerColliders = new List<Collider>();

    protected override void UpdateDetection()
    {
        detectedPlayerColliders.Clear();

        detectedTarget = null;

        Collider[] colls = Physics.OverlapSphere(transform.position, radius);

        foreach(var coll in colls)
            if(coll.gameObject.tag == targetTag)
                detectedPlayerColliders.Add(coll);

        if (detectedPlayerColliders.Count == 0)
            return;
        if (detectedPlayerColliders.Count > 1)
            detectedTarget = Random.value > 0.5f ? detectedPlayerColliders[0].transform : detectedPlayerColliders[1].transform;
        else
            detectedTarget = detectedPlayerColliders[0].transform;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
