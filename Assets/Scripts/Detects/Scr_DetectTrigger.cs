using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DetectTrigger : Scr_Detect
{
    public Scr_TriggerDetection triggerDetection = null;

    protected override void UpdateDetection()
    {
        if(triggerDetection.detectedTriggers.Count > 0)
        {
            bool targetTagTrigger = false;
            foreach(var detectedTrigger in triggerDetection.detectedTriggers)
            {
                if(detectedTrigger.tag == targetTag)
                {
                    detectedTarget = detectedTrigger;
                    targetTagTrigger = true;
                }
            }

            if(!targetTagTrigger) detectedTarget = null;
        }
        else
        {
            detectedTarget = null;
        }
    }
}
