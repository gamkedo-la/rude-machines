using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_FloatingMotion : MonoBehaviour
{
    public float heaveDistance = 0.5f;
    public float heaveSpeed = 20.0f;
    [Space]
    public float rotationSpeed = 0.1f;

    private float randomFactor = 0.0f;

    void Start()
    {
        randomFactor = Random.value * 600.0f;
    }

    void Update()
    {
        Vector3 position = transform.localPosition;
        position.y = Mathf.Sin((Time.time + randomFactor) * heaveSpeed) * heaveDistance;
        transform.localPosition = position;

        Vector3 rotation = transform.localRotation.eulerAngles;
        rotation.y += rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(rotation);
    }
}
