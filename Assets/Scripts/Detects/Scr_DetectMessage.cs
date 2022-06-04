using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DetectMessage : Scr_Detect
{
    public Scr_Detect detectorContainingStateMessage;
    [HideInInspector] public string message = "";

    //detectedTarget gets set via external scripts
    protected override void UpdateDetection()
    {
        if(detectorContainingStateMessage.detectedTarget == null) return;
        var stateMessage = detectorContainingStateMessage.detectedTarget.GetComponent<Scr_StateMessage>();
        if(stateMessage.enabled)
        {
            detectedTarget = stateMessage.GetDetectedTarget();
            stateMessage.StateTerminate();
            //Debug.Log(stateMessage.message);
        }
    }
}
