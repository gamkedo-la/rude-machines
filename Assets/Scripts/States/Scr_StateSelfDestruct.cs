using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateSelfDestruct : Scr_State
{
    public float delay = 2.0f;
    public Scr_Detect detector = null;

    private float timer = 0.0f;

    protected override void StateInitialize()
    {
        detector = GetComponent<Scr_Detect>();
        timer = delay;
    }

    protected override void StateActivity()
    {
        if (detector!=null && detector.detectedTarget != null)
        {
            if (timer <= 0.0f)
            {
                Destroy(this.gameObject);
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            timer = delay;
        }
    }
}
