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

    private void OnCollisionEnter(Collision collision)
    {
        Scr_IDamageable damageable = collision.gameObject.GetComponentInParent<Scr_IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(0.25f, gameObject);
            collision.transform.parent.parent.position += (collision.transform.parent.parent.position - transform.position) / 4.0f;
        }
        Destroy(gameObject);
    }
}
