using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BackAndForth : MonoBehaviour
{
    public float xMinAngle = 7.5f;
    public float xMaxAngle = 7.5f;
    public float xWobbleSpeed = 1f;
    public float yMinAngle = 170f;
    public float yMaxAngle = 190f;
    public float yWobbleSpeed = 1f;
    public float zMinAngle = 0f;
    public float zMaxAngle = 0f;
    public float zWobbleSpeed = 1f;
    public TrailRenderer trail = null;

    private float prevRotY = 0.0f;

    void Update()
    {
        float rotX = Mathf.SmoothStep(xMinAngle, xMaxAngle, Mathf.PingPong(Time.time * xWobbleSpeed, 1));
        float rotY = Mathf.SmoothStep(yMinAngle, yMaxAngle, Mathf.PingPong(Time.time * yWobbleSpeed, 1));
        float rotZ = Mathf.SmoothStep(zMinAngle, zMaxAngle, Mathf.PingPong(Time.time * zWobbleSpeed, 1));

        if (trail != null)
        {
            float yRotDiff = rotY < prevRotY ? Mathf.Abs(rotY - yMinAngle) : Mathf.Abs(rotY - yMaxAngle);
            trail.time = Mathf.Lerp(trail.time, 1.0f - (yRotDiff / (yMaxAngle - yMinAngle)), 12.0f * Time.deltaTime);
        }

        transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ);
        prevRotY = rotY;
    }
}
