using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Projectile : MonoBehaviour
{
    public float force = 10.0f;

    private GameObject self;
    private Rigidbody rb;

    public void SetGameobject(GameObject obj)
    {
        self = obj;
    }

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
        if (collision.gameObject != self && collision.gameObject.tag != "Platform")
        {
            Scr_IDamageable damageable = collision.gameObject.GetComponentInParent<Scr_IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(0.25f, gameObject);
            }
            Destroy(gameObject);
        }
    }
}
