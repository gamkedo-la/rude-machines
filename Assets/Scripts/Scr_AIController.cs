using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AIController : MonoBehaviour
{
    public Vector3 targetPosition;
    public float moveThreshold = 4.0f;
    public float moveSpeed = 10.0f;

    private Vector3 moveDirection = Vector3.zero;

    public void Move(Vector3 position)
    {
        targetPosition = position;
    }

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        Vector3 position = transform.position;
        if(Vector3.Distance(position, targetPosition) > moveThreshold)
        {
            position = Vector3.Lerp(position, targetPosition, moveSpeed * Time.deltaTime);
        }
        moveDirection = Vector3.Normalize(position - transform.position);
        transform.rotation = Quaternion.LookRotation(moveDirection);
        transform.position = Vector3.Lerp(transform.position, position, 10.0f * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + moveDirection);
    }
}
