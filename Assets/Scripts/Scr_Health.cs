using UnityEngine;

public class Scr_Health : Scr_BarProperty, Scr_IDamageable
{
    public void Damage(float value, GameObject instigator)
    {
        Value -= value;
    }
}
