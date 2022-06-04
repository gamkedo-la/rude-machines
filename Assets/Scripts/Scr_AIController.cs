using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AIController : MonoBehaviour
{
    public Vector3 targetPosition;
    public float moveThreshold = 4.0f;
    public float moveSpeed = 10.0f;
    public bool fixYPosition = false;
    public float yPositionLimit = 0.5f;

    public enum RotationUpdateType
    {
        YAW,
        YAW_TILT
    }

    [Space]
    public RotationUpdateType rotation = RotationUpdateType.YAW;
    public float tiltRatioFactor = 1.0f;
    public Transform forwardRotationPoint;
    public Transform rotationTarget = null;

    private Vector3 moveDirection = Vector3.zero;
    private float fixedY = 0.0f;

    public void Move(Vector3 position)
    {
        targetPosition = position;
    }

    void Start()
    {
        targetPosition = transform.position;
        fixedY = transform.position.y;
    }

    void UpdateDirectionalTilt(Vector3 position)
    {
        moveDirection = Vector3.Normalize(position - transform.position);
        if(moveDirection.magnitude == 0.0f){
            return;
        }
        Vector3 moveRotation = Quaternion.LookRotation(moveDirection).eulerAngles;
        Vector3 forwardRotation = Quaternion.LookRotation(forwardRotationPoint.position).eulerAngles;
        float ratio = (rotation == RotationUpdateType.YAW_TILT)
            ? Mathf.Clamp(Vector3.Distance(position, transform.position) * tiltRatioFactor, 0.0f, 1.0f)
            : 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(forwardRotation.x * ratio, moveRotation.y, forwardRotation.z * ratio), 5.0f * Time.deltaTime);
    }

    void Update()
    {
        Vector3 position = transform.position;
        if (Vector3.Distance(position, targetPosition) > moveThreshold)
        {
            position = Vector3.Lerp(position, targetPosition, moveSpeed * Time.deltaTime);
        }
        
        if (position.y < yPositionLimit) position.y = yPositionLimit;
        if (fixYPosition) position.y = fixedY;

        UpdateDirectionalTilt(rotationTarget == null ? position : rotationTarget.position);

        transform.position = Vector3.Lerp(transform.position, position, 10.0f * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + moveDirection);
    }
}
