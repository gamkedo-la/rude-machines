using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateAttackDetectedTarget : Scr_State
{
    public GameObject projectile;
    public Transform muzzle;
    public GameObject muzzleParticle;
    [Space]
    public float detectionDelay = 2.0f;
    public float shotSpreadAngle = 15.0f;
    public float shotDelay = 0.1f;
    private float timer = 0.0f;
    private int shotsLeft = 0;

    public override void StateInitialize()
    {
        base.StateInitialize();
        timer = detectionDelay;
    }

    protected override void StateActivity()
    {
        if (timer <= 0.0f) // ready to fire
        {
            timer = shotDelay;
            Shoot();
        }
        else
        {
            // countdown to shooting the target or next bullet
            timer -= Time.deltaTime;
        }
    }

    public override void StateTerminate()
    {
        base.StateTerminate();
        muzzleParticle.SetActive(false); // not shooting
        timer = detectionDelay; // no target: re-fill detection timer
    }

    void Shoot()
    {
        muzzleParticle.SetActive(true);
        Vector3 rot = muzzle.rotation.eulerAngles;
        GameObject newProjectile = Instantiate(projectile, muzzle.position, Quaternion.Euler(rot.x + Random.Range(-shotSpreadAngle, shotSpreadAngle), rot.y + Random.Range(-shotSpreadAngle, shotSpreadAngle), rot.z));
        newProjectile.GetComponent<Scr_Projectile>().SetGameobject(gameObject);
    }
}
