using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Wall : MonoBehaviour
{
    public float waitTimer = 1.0f;
    public float delay = 1.0f;
    [Space]
    public AnimationCurve upCurve;
    public float upCurveIntensity = 1.0f;

    private Vector3 basePosition = Vector3.zero;
    private Collider coll = null;
    private float timer = 0.0f;

    void Awake()
    {
        basePosition = transform.localPosition;
        Vector3 position = transform.localPosition;
        position.y = -1.0f;
        transform.localPosition = position;
    }

    void Start()
    {
        coll = GetComponent<Collider>();
        timer = delay;

        coll.enabled = false;
    }

    void Update()
    {
        if(waitTimer <= 0.0f)
        {
            coll.enabled = true;

            if(timer > 0.0f)
            {
                Vector3 position = transform.localPosition;
                position.y = basePosition.y + (upCurve.Evaluate(1.0f - (timer/delay)) * upCurveIntensity);
                transform.localPosition = position;

                timer -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Vector3 position = transform.localPosition;
            position.y = Mathf.Lerp(position.y, basePosition.y, 8.0f * Time.deltaTime);
            transform.localPosition = position;

            waitTimer -= Time.deltaTime;
        }
    }
}
