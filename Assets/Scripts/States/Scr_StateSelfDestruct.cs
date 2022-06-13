using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateSelfDestruct : Scr_State
{
    [Space]
    public float delay = 2.0f;
    public GameObject destroyObject;

    private float timer = 0.0f;

    public override void StateInitialize()
    {
        base.StateInitialize();
        timer = delay;
    }

    protected override void StateActivity()
    {
        if (timer <= 0.0f)
        {
            detector.detectedTarget.GetComponent<Scr_IDamageable>().Damage(0.5f, gameObject);
            Instantiate(destroyObject, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else timer -= Time.deltaTime;
    }

    public override void StateTerminate()
    {
        base.StateTerminate();
        timer = delay;
    }
}
