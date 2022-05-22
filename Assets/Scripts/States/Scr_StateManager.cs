using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateManager : MonoBehaviour
{
    private Scr_AIController controller;
    private Scr_StateIdle idle;
    private Scr_StateRevolve revolve;

    void Start()
    {
        controller = GetComponent<Scr_AIController>();
        idle = GetComponent<Scr_StateIdle>();
        revolve = GetComponent<Scr_StateRevolve>();
    }

    void Update()
    {
        
    }
}
