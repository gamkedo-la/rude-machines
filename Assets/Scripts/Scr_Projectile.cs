using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Projectile : MonoBehaviour
{
    public float damageValue = 0.25f;
    public float force = 10.0f;
    public GameObject damageSphere;

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
        if (collision.gameObject != self
        && collision.gameObject.GetComponent<Scr_Projectile>() == null
        && collision.gameObject.tag != "Platform")
        {
            Scr_IDamageable damageable = collision.gameObject.GetComponentInParent<Scr_IDamageable>();
            if (damageable != null)
            {
                GameObject dmgSphere = Instantiate(damageSphere, transform.position, Quaternion.identity);

                float dmg = damageValue;
                for(int i = 0; i < collision.contactCount; i++)
                {
                    if(collision.contacts[i].thisCollider.name == "WEAK"
                    || collision.contacts[i].otherCollider.name == "WEAK")
                    {
                        dmg *= 2.0f;
                        dmgSphere.transform.localScale *= 2.0f;
                        break;
                    }
                    else if(collision.contacts[i].thisCollider.name == "STRONG"
                    || collision.contacts[i].otherCollider.name == "STRONG")
                    {
                        dmg /= 2.0f;
                        dmgSphere.transform.localScale /= 2.0f;
                        break;
                    }
                }
                damageable.Damage(dmg, self);
            }
            Destroy(gameObject);
        }
    }
}
