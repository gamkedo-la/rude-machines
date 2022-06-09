using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_PlayerHealth : Scr_Health
{
    [Space]
    public float regenerationRate = 0.05f;
    public ParticleSystem damageIndicator;
    public float maxCircleRadius = 0.3f;
    [Space]
    public Camera cam;
    public float damageFOV = 24.0f;

    private float defaultFOV = 72.0f;

    void UpdateDamageIndicator()
    {
        var damageIndicatorShape = damageIndicator.shape;
        damageIndicatorShape.radius = Value * maxCircleRadius;
    }

    void Update()
    {
        Value += regenerationRate * Time.deltaTime;
        UpdateDamageIndicator();

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, defaultFOV, Time.deltaTime * 8.0f);
    }

    override public void Damage(float value, GameObject instigator)
    {
        Value -= value;
        UpdateDamageIndicator();

        cam.fieldOfView = damageFOV;

        if(Value <= 0.0f) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
