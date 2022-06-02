using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateSelfDestruct : Scr_State
{
    public float delay = 2.0f;

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
            detector.detectedTarget.GetComponent<Scr_IDamageable>().Damage(0.4f, gameObject);
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
