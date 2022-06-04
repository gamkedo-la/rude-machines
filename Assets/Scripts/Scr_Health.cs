using UnityEngine;

public class Scr_Health : Scr_BarProperty, Scr_IDamageable
{
    public float damageFactor = 1.0f;

    virtual public void Damage(float value, GameObject instigator)
    {
        Value -= value * damageFactor;
        if(Value <= 0) Destroy(gameObject);
    }
}
