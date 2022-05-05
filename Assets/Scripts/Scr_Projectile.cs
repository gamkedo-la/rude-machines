using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Projectile : MonoBehaviour
{
    public float force = 10.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    void Update()
    {
        
    }
}
