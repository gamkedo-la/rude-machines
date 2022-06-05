using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GameManager : MonoBehaviour
{
    public static Scr_GameManager instance = null;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
