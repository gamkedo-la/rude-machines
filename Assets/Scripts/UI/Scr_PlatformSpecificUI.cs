using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlatformSpecificUI : MonoBehaviour
{
    public RuntimePlatform[] exclusionPlatforms;
    void Awake()
    {
        foreach (var p in exclusionPlatforms)
        {
            if (Application.platform == p)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
