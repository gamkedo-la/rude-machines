using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Follow : MonoBehaviour
{
    public string targetTag = "Player";
    public Vector3 offset = Vector3.zero;
    public float forwardOffset = 0.0f;

    private Transform target = null;

    void Update()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag(targetTag)?.transform;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset + (target.forward * forwardOffset), 12.0f * Time.deltaTime);

            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y = target.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
