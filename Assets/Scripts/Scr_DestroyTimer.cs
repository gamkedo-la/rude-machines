using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DestroyTimer : MonoBehaviour
{
    public float delay = 5.0f;

    void Update()
    {
        if(delay <= 0.0f) Destroy(gameObject);
        else delay -= Time.deltaTime;
    }
}
