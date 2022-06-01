using UnityEngine;

public class Scr_Health : Scr_BarProperty, Scr_IDamageable
{
    virtual public void Damage(float value, GameObject instigator)
    {
        Value -= value;
        if(Value <= 0) Destroy(gameObject);
    }
}
