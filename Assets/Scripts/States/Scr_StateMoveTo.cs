using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateMoveTo : Scr_State
{
    [Space]
    public Vector3 targetPosition = Vector3.zero;

    private Scr_AIController controller;

    public override void StateInitialize()
    {
        base.StateInitialize();
        controller = GetComponent<Scr_AIController>();
    }



    protected override void StateActivity()
    {
        controller.targetPosition = targetPosition;
    }
}
