using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateStrafe : Scr_State
{
    [Space]
    public float minDistance = 4.0f;
    public float maxDistance = 8.0f;
    public float angleRate = 4.0f;

    private float angle = 0.0f;

    private Scr_AIController controller;
    private float distance = 0.0f;

    public override void StateInitialize()
    {
        base.StateInitialize();
        controller = GetComponent<Scr_AIController>();
        distance = Random.Range(minDistance, maxDistance);

        angle = Vector3.Angle(transform.position, detector.detectedTarget.position) - 90.0f;
        angleRate = Random.value < 0.5 ? -angleRate : angleRate;
    }

    protected override void StateActivity()
    {
        if (controller == null || detector == null || detector.detectedTarget == null)
        {
            StateTerminate();
            return;
        }

        Quaternion origRotation = detector.detectedTarget.rotation;

        Vector3 rotation = detector.detectedTarget.rotation.eulerAngles;
        rotation.y = angle;
        detector.detectedTarget.rotation = Quaternion.Euler(rotation);

        controller.targetPosition = detector.detectedTarget.position + (detector.detectedTarget.forward * distance);
        controller.rotationTarget = detector.detectedTarget;

        detector.detectedTarget.rotation = origRotation;

        angle += (angleRate * Time.deltaTime);
    }

    public override void StateTerminate()
    {
        base.StateTerminate();
        if (controller != null) controller.rotationTarget = null;
    }

    /*
    public void OnDrawGizmos()
    {
        if (controller == null || detector == null || detector.detectedTarget == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(controller.targetPosition, 1.0f);
    }
    */
}
