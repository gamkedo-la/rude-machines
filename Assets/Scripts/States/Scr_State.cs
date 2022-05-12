using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_State : MonoBehaviour
{
    [Header("Light Properties")]
    public Transform lightGroup;
    [ColorUsageAttribute(true, true)] public Color lightColor = Color.white;
    public float lightBlinkDelay = 0.0f;

    private List<MeshRenderer> lights = new List<MeshRenderer>();
    private float lightBlinkTimer = 0.0f;

    private bool lightAlternate = false;

    void OnValidate()
    {
        if (lights.Count <= 0) Start();

        foreach (var l in lights)
        {
            l.sharedMaterial.SetColor("_BaseColor", lightColor);
            l.sharedMaterial.SetColor("_EmissionColor", lightColor);
        }
    }

    protected virtual void StateInitialize() { }

    void Start()
    {
        StateInitialize();
        lights.Clear();
        foreach (var l in lightGroup.GetComponentsInChildren<MeshRenderer>())
        {
            lights.Add(l);
        }
    }

    void Update()
    {
        StateActivity();
        UpdateLight();
    }

    protected virtual void StateActivity() { }

    void UpdateLight()
    {
        if (lightBlinkDelay <= 0.0f) return;

        if(lightBlinkTimer <= 0.0f)
        {
            foreach(var l in lights)
            {
                l.material.SetColor("_BaseColor", lightAlternate ? lightColor : Color.black);
                l.material.SetColor("_EmissionColor", lightAlternate ? lightColor : Color.black);
            }
            lightAlternate = !lightAlternate;
            lightBlinkTimer = lightBlinkDelay;
        }
        else
        {
            lightBlinkTimer -= Time.deltaTime;
        }
    }
}
