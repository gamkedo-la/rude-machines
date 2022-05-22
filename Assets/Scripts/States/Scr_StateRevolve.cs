using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateRevolve : Scr_State
{
    [Space]
    public Transform target;
    public float rotationSpeed = 2.0f;
    public float minDistanceFromTarget = 6.0f;
    public float maxDistanceFromTarget = 12.0f;

    private Scr_AIController controller;
    private float rotation = 0.0f;
    private float distance = 0.0f;
    private Vector3 offset = Vector3.zero;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    protected override void StateInitialize()
    {
        controller = GetComponent<Scr_AIController>();
        rotation = Random.value * 360.0f;
        distance = Random.Range(minDistanceFromTarget, maxDistanceFromTarget);
        offset = new Vector3(Random.value * 1.0f, Random.value * 1.0f, Random.value * 1.0f);
    }

    protected override void StateActivity()
    {
        if(target != null)
        {
            rotation += rotationSpeed * Time.deltaTime;
            Quaternion targetRotation = target.rotation;
            target.rotation = Quaternion.Euler(targetRotation.x, rotation, targetRotation.z);
            controller.targetPosition = target.position + (target.forward * distance) + offset;
            target.rotation = targetRotation;
        }
    }
}
