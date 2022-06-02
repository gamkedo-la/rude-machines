using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateMessage : Scr_State
{
    public int minCount = 1;
    public int maxCount = 4;
    [HideInInspector] public int count = 0;

    public Transform GetDetectedTarget() { return detector.detectedTarget; }

    public override void StateInitialize()
    {
        base.StateInitialize();
        count = Random.Range(minCount, maxCount);
    }

    protected override void StateActivity()
    {
    }

    public override void StateTerminate()
    {
        if(count > 0)
            count--;
        else
        {
            base.StateTerminate();
            detector.detectedTarget = null;
        }
    }
}
