using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateIdle : Scr_State
{
    public float delay = 0.5f;
    public float distance = 2.0f;

    private Scr_AIController controller;
    private Vector3 startingPosition;
    private float timer = 0.0f;

    public override void StateInitialize()
    {
        base.StateInitialize();
        controller = GetComponent<Scr_AIController>();
        startingPosition = transform.position;
        timer = Random.value * delay;
    }

    protected override void StateActivity()
    {
        if (timer <= 0.0f)
        {
            controller.targetPosition = startingPosition + (Random.onUnitSphere * distance);
            timer = delay;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
