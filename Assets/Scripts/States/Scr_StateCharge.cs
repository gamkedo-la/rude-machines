using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateCharge : Scr_State
{
    [Space]
    public float variationOffsetFactor = 0.0f;
    public float variationOffsetChangeDelay = 0.5f;

    private Scr_AIController controller;
    private float variationOffsetChangeTimer = 0.0f;
    private Vector3 variationOffset = Vector3.zero;

    public override void StateInitialize()
    {
        base.StateInitialize();
        controller = GetComponent<Scr_AIController>();

        variationOffset = new Vector3(((Random.value - 0.5f) * 2.0f) * variationOffsetFactor,
                                    ((Random.value - 0.5f) * 2.0f) * variationOffsetFactor,
                                    ((Random.value - 0.5f) * 2.0f) * variationOffsetFactor);
        variationOffsetChangeTimer = Random.value * variationOffsetChangeDelay;
    }

    protected override void StateActivity()
    {
        if(controller == null || detector == null || detector.detectedTarget == null)
        {
            StateTerminate();
            return;
        }

        controller.targetPosition = detector.detectedTarget.position + variationOffset;

        if(variationOffsetChangeTimer <= 0.0f)
        {
            variationOffset = new Vector3(((Random.value - 0.5f) * 2.0f) * variationOffsetFactor,
                                    ((Random.value - 0.5f) * 2.0f) * variationOffsetFactor,
                                    ((Random.value - 0.5f) * 2.0f) * variationOffsetFactor);
            variationOffsetChangeTimer = variationOffsetChangeDelay;
        }
        else
        {
            variationOffsetChangeTimer -= Time.deltaTime;
        }
    }

    public override void StateTerminate()
    {
        base.StateTerminate();
    }
}
