using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Scr_GameManager : MonoBehaviour
{
    [SerializeField] private Volume stuckTimeVolume;

    public static Scr_GameManager instance = null;

    float preStuckTime = 0.0f, stuckTime = 0.0f;

    public void SetStuckTime(float time, float pre = 0)
    {
        stuckTime = time;
        preStuckTime = pre;
    }

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if(preStuckTime <= 0.0f)
        {
            if(stuckTime <= 0.0f)
            {
                Time.timeScale = 1.0f;
                stuckTimeVolume.weight = 0.0f;
            }
            else
            {
                Time.timeScale = 0.0f;
                stuckTimeVolume.weight = 1.0f;
                stuckTime -= Time.unscaledDeltaTime;
            }
        }
        else
        {
            preStuckTime -= Time.deltaTime;
        }
    }
}
