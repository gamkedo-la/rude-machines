using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_State : MonoBehaviour
{
    [Header("Pre State")]
    [SerializeField] public float preStateMinDelay = 0.0f;
    [SerializeField] public float preStateMaxDelay = 0.2f;

    [Header("Light Properties")]
    public Transform lightGroup;
    [ColorUsageAttribute(true, true)] public Color lightColor = Color.white;
    public float lightBlinkDelay = 0.0f;

    private float preStateTimer = 0.0f;
    private List<MeshRenderer> lights = new List<MeshRenderer>();
    private float lightBlinkTimer = 0.0f;
    private bool lightAlternate = false;
    protected Scr_Detect detector = null;

    public void SetDetector(Scr_Detect newDetector)
    {
        detector = newDetector;
    }

    public virtual void StateInitialize()
    {
        this.enabled = true;
        preStateTimer = Random.Range(preStateMinDelay, preStateMaxDelay);
    }

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
        if(preStateTimer <= 0.0f) StateActivity();
        else preStateTimer -= Time.deltaTime;

        UpdateLight();
    }

    protected virtual void StateActivity()
    {
        //nothing
    }

    public virtual void StateTerminate()
    {
        this.enabled = false;
    } 

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
