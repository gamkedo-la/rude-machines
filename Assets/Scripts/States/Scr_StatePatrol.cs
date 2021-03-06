using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StatePatrol : Scr_State
{
    [Space]
    public float patrolPointDistanceThreshold = 1.0f;
    public enum Mode
    {
        NORMAL,
        REVERSE,
        PING_PONG
    }
    public Mode mode = Mode.NORMAL;
    public float fixedY = -1.0f;

    private Transform patrolPointGroup = null;
    private Scr_AIController controller;
    private int patrolPointIndex = 0;
    private bool pingPongAlternate = false;

    void GetClosestPatrolPointInGroup()
    {
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

    public override void StateInitialize()
    {
        base.StateInitialize();
        controller = GetComponent<Scr_AIController>();

        patrolPointGroup = Scr_GameManager.instance.patrolPointGroups.GetChild(Random.Range(0, Scr_GameManager.instance.patrolPointGroups.childCount));
        GetClosestPatrolPointInGroup();
    }

    protected override void StateActivity()
    {
        controller.targetPosition = patrolPointGroup.GetChild(patrolPointIndex).position;
        if (fixedY > 0.0f) controller.targetPosition.y = fixedY;

        if(Vector3.Distance(transform.position, controller.targetPosition) <= patrolPointDistanceThreshold)
        {
            if(Random.value < 0.25f)
            {
                patrolPointGroup = Scr_GameManager.instance.patrolPointGroups.GetChild(Random.Range(0, Scr_GameManager.instance.patrolPointGroups.childCount));
                GetClosestPatrolPointInGroup();
            }
            else
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
                }
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
