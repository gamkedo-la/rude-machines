using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateStun : Scr_State
{
    [Space]
    public GameObject indicator = null;

    private Scr_AIController controller;

    public override void StateInitialize()
    {
        base.StateInitialize();
        controller = GetComponent<Scr_AIController>();
        controller.rotationTarget = null;

        if (indicator != null)
            indicator.SetActive(true);
    }

    protected override void StateActivity()
    {
        base.StateActivity();
        //nothing at all!
    }

    public override void StateTerminate()
    {
        base.StateTerminate();
        if(indicator != null)
            indicator.SetActive(false);
    }
}
