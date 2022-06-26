using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DetectHitPattern : Scr_Detect
{
    public Scr_CollisionDetectionForPattern[] pattern;
    public float patternHitWithinTime = 5.0f;
    public float delayAfterDetectingTarget = 0.0f;

    private Scr_Health health;
    private int patternIndex = 0;
    private float patternHitWithinTimer = 0.0f;
    private float detectedTargetTimer = 0.0f;
    private float maxHealthBeforePattern = 0.99f;

    private void Start()
    {
        health = GetComponent<Scr_Health>();
        patternHitWithinTimer = patternHitWithinTime;
    }

    void ResetPattern()
    {
        for (int p = 0; p < pattern.Length; p++)
        {
            pattern[p].collided = false;
            pattern[p].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    protected override void UpdateDetection()
    {
        if (health.Value >= maxHealthBeforePattern) return;

        if (patternIndex < pattern.Length)
        {
            pattern[patternIndex].transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            ResetPattern();
            patternIndex = 0;
            maxHealthBeforePattern = health.Value - 0.01f;
            patternHitWithinTimer = patternHitWithinTime;

            detectedTarget = transform;
            detectedTargetTimer = delayAfterDetectingTarget;
        }

        if (pattern[patternIndex].collided)
        {
            ResetPattern();
            patternIndex++;
        }
        else if(patternHitWithinTimer <= 0.0f)
        {
            ResetPattern();
            patternIndex = 0;
            maxHealthBeforePattern = health.Value - 0.01f;
            patternHitWithinTimer = patternHitWithinTime;
        }
        else
        {
            patternHitWithinTimer -= Time.deltaTime;
        }

        if (detectedTargetTimer <= 0.0f) detectedTarget = null;
        else detectedTargetTimer -= delay;
    }
}
