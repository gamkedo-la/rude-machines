using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateManager : MonoBehaviour
{
    private Scr_AIController controller;
    private Scr_StateIdle idle;
    private Scr_StateRevolve revolve;
    private Scr_StateSelfDestruct selfDestruct;
    private Scr_Detect detector;

    void Start()
    {
        controller = GetComponent<Scr_AIController>();
        idle = GetComponent<Scr_StateIdle>();
        revolve = GetComponent<Scr_StateRevolve>();
        selfDestruct = GetComponent<Scr_StateSelfDestruct>();
        detector = GetComponent<Scr_Detect>();
    }

    void Update()
    {
        if(detector.detectedTarget != null)
        {
            revolve.enabled = false;
            selfDestruct.enabled = true;
        }
        else
        {
            revolve.enabled = true;
            selfDestruct.enabled = false;
        }
    }
}
