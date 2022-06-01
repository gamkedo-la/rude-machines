using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_PlayerHealth : Scr_Health
{
    public float regenerationRate = 0.05f;
    public ParticleSystem damageIndicator;
    public float maxCircleRadius = 0.3f;

    void UpdateDamageIndicator()
    {
        var damageIndicatorShape = damageIndicator.shape;
        damageIndicatorShape.radius = Value * maxCircleRadius;
    }

    void Update()
    {
        Value += regenerationRate * Time.deltaTime;
        UpdateDamageIndicator();
    }

    override public void Damage(float value, GameObject instigator)
    {
        Value -= value;
        UpdateDamageIndicator();

        if(Value <= 0.0f) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
