using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Detect : MonoBehaviour
{
    public string targetTag = "";
    public float delay = 0.1f;

    [HideInInspector] public Transform detectedTarget = null;
    
    protected float timer = 0.0f;

    protected virtual void UpdateDetection() {}

    void Update()
    {
        if (timer <= 0.0f)
        {
            UpdateDetection();
            timer = delay;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
