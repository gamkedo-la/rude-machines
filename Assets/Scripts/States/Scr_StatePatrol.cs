using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StatePatrol : Scr_State
{
    public Transform patrolPointGroup = null;
    public float patrolPointDistanceThreshold = 1.0f;
    public enum Mode
    {
        NORMAL,
        REVERSE,
        PING_PONG
    }
    public Mode mode = Mode.NORMAL;

    private Scr_AIController controller;
    private int patrolPointIndex = 0;
    private bool pingPongAlternate = false;

    public override void StateInitialize()
    {
        base.StateInitialize();
        controller = GetComponent<Scr_AIController>();

        //Start from the closest patrol point...
        float currentDistance = 99999.0f;
        for(int i = 0; i < patrolPointGroup.childCount; i++)
        {
            float distance = Vector3.Distance(transform.position, patrolPointGroup.GetChild(i).position);
            if(distance < currentDistance)
            {
                currentDistance = distance;
                patrolPointIndex = i;
            }
        }
    }

    protected override void StateActivity()
    {
        controller.targetPosition = patrolPointGroup.GetChild(patrolPointIndex).position;

        if(Vector3.Distance(transform.position, controller.targetPosition) <= patrolPointDistanceThreshold)
        {
            switch(mode)
            {
                case Mode.NORMAL:
                    patrolPointIndex++;
                    if(patrolPointGroup.childCount <= patrolPointIndex) patrolPointIndex = 0;
                break;
                case Mode.REVERSE:
                    patrolPointIndex--;
                    if(patrolPointIndex < 0) patrolPointIndex = patrolPointGroup.childCount - 1;
                break;
                case Mode.PING_PONG:
                    patrolPointIndex += pingPongAlternate ? 1 : -1;
                    if(patrolPointIndex == patrolPointGroup.childCount - 1) pingPongAlternate = false;
                    else if(patrolPointIndex == 0) pingPongAlternate = true;
                break;
            }
        }
    }

    void OnDrawGizmos()
    {
        if(patrolPointGroup == null) return;

        Gizmos.color = Color.yellow;
        for(int i = 0; i < patrolPointGroup.childCount - 1; i++)
        {
            Gizmos.DrawLine(patrolPointGroup.GetChild(i).position, patrolPointGroup.GetChild(i+1).position);
        }

        Gizmos.DrawLine(patrolPointGroup.GetChild(0).position, patrolPointGroup.GetChild(patrolPointGroup.childCount - 1).position);
    }
}
