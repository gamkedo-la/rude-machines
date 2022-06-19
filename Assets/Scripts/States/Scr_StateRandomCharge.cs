using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateRandomCharge : Scr_State
{
    [Space]
    [SerializeField] float radius = 20.0f;
    [SerializeField] float rotationRate = 150.0f;
    [SerializeField] float reflectionDeviation = 0.2f;
    [SerializeField] float targetPositionDistance = 2.0f;

    private Scr_AIController controller;
    private Quaternion newRotation;
    private float forceMoveTimer = 0.0f;

    public override void StateInitialize()
    {
        base.StateInitialize();
        controller = GetComponent<Scr_AIController>();

        newRotation = Quaternion.LookRotation(-transform.forward + (transform.right * ((Random.value - 0.5f) * 2.0f) * reflectionDeviation));
    }

    protected override void StateActivity()
    {
        if (transform.rotation != newRotation)
        {
            controller.Stop();
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * rotationRate);
            forceMoveTimer = 1.0f;
        }
        else if (forceMoveTimer <= 0.0f && Vector3.Distance(transform.position, Vector3.zero) >= radius)
        {
            controller.Stop();
            newRotation = Quaternion.LookRotation(-transform.forward + (transform.right * ((Random.value - 0.5f) * 2.0f) * reflectionDeviation));
        }
        else
        {
            controller.targetPosition = transform.position + (transform.forward * targetPositionDistance);
            forceMoveTimer -= Time.deltaTime;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Vector3.zero + Vector3.up, radius);
    }
}
