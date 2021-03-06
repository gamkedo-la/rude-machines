using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateShoot : Scr_State
{
    [Space]
    public GameObject projectile;
    public Transform[] muzzle;
    public GameObject[] muzzleParticle;
    [Space]
    public float detectionDelay = 2.0f;
    public float shotSpreadAngle = 15.0f;
    public float shotDelay = 0.1f;
    [Space]
    public float shotStartingSpreadAngle = 50.0f;
    public float shotSpreadAngleNormalizeDelay = 0.0f;

    private Scr_AIController controller;

    private float timer = 0.0f;
    private int shotsLeft = 0;

    private float shotSpreadAngleNormalizeTimer = 0.0f;

    private int currentMuzzleIndex = 0;

    public override void StateInitialize()
    {
        base.StateInitialize();
        controller = GetComponent<Scr_AIController>();
        timer = detectionDelay;
        shotSpreadAngleNormalizeTimer = shotSpreadAngleNormalizeDelay;
    }

    protected override void StateActivity()
    {
        controller.rotationTarget = detector.detectedTarget;

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
        for(int i = 0; i < muzzle.Length; i++)
            muzzleParticle[i].SetActive(false); // not shooting
        timer = detectionDelay; // no target: re-fill detection timer
        if(controller != null) controller.rotationTarget = null;
    }

    void Shoot()
    {
        for(int i = 0; i < muzzle.Length; i++)
        {
            muzzleParticle[i].SetActive(true);
            Vector3 rot = muzzle[i].rotation.eulerAngles;
            float spreadAngle = Mathf.Lerp(shotSpreadAngle, shotStartingSpreadAngle, shotSpreadAngleNormalizeDelay != 0.0f ? shotSpreadAngleNormalizeTimer / shotSpreadAngleNormalizeDelay : 0.0f);
            if (spreadAngle == float.NaN) spreadAngle = shotSpreadAngle;
            GameObject newProjectile = Instantiate(projectile, muzzle[i].position, Quaternion.Euler(rot.x + Random.Range(-spreadAngle, spreadAngle), rot.y + Random.Range(-spreadAngle, spreadAngle), rot.z));
            newProjectile.GetComponent<Scr_Projectile>().SetGameobject(gameObject);
        }
        shotSpreadAngleNormalizeTimer -= Time.deltaTime;
    }
}
