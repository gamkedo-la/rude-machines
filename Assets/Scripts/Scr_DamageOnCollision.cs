using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DamageOnCollision : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private float damageRate = 0.2f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            Scr_IDamageable damageable = collision.gameObject.GetComponentInParent<Scr_IDamageable>();
            if (damageable != null) damageable.Damage(damageRate * Time.deltaTime, gameObject);
        }
    }
}
