using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DamageOnCollision : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private float damageRate = 0.2f;
    [SerializeField] private float delay = 0.0f;

    public float DamageRate { get { return damageRate; } set { damageRate = value; } }

    private float timer = 0.0f;
    private GameObject ownerObject = null;

    public void SetOwner(GameObject obj)
    {
        ownerObject = obj;
    }

    private void Start()
    {
        if(ownerObject == null) ownerObject = gameObject;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    private void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.tag == targetTag || targetTag == "Any")
        {
            if (timer <= 0.0f)
            {
                Scr_IDamageable damageable = coll.gameObject.GetComponentInParent<Scr_IDamageable>();
                if (damageable == null && coll.transform.parent != null) damageable = coll.gameObject.GetComponentInParent<Scr_IDamageable>();
                if (damageable == null && coll.transform.parent != null && coll.transform.parent.parent != null) damageable = coll.transform.parent.GetComponentInParent<Scr_IDamageable>();

                if (damageable != null) damageable.Damage(damageRate * Time.deltaTime, ownerObject);

                Scr_CollisionDetectionForPattern collisionDetectionForPattern = coll.gameObject.GetComponent<Scr_CollisionDetectionForPattern>();
                if (collisionDetectionForPattern != null) collisionDetectionForPattern.collided = true;

                timer = delay;
            }
        }
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.tag == targetTag || targetTag == "Any")
        {
            if (timer <= 0.0f)
            {
                Scr_IDamageable damageable = coll.GetComponent<Scr_IDamageable>();
                if (damageable == null && coll.transform.parent != null) damageable = coll.GetComponentInParent<Scr_IDamageable>();
                if (damageable == null && coll.transform.parent != null && coll.transform.parent.parent != null) damageable = coll.transform.parent.GetComponentInParent<Scr_IDamageable>();

                if (damageable != null) damageable.Damage(damageRate * Time.deltaTime, ownerObject);

                Scr_CollisionDetectionForPattern collisionDetectionForPattern = coll.GetComponent<Scr_CollisionDetectionForPattern>();
                if (collisionDetectionForPattern != null) collisionDetectionForPattern.collided = true;

                timer = delay;
            }
        }
    }
}
