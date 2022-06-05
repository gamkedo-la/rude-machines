using UnityEngine;

public class Scr_Health : Scr_BarProperty, Scr_IDamageable
{
    [Space]
    public float damageFactor = 1.0f;
    public GameObject damageEffect = null;
    public GameObject destroyEffect = null;

    virtual public void Damage(float value, GameObject instigator)
    {
        Value -= value * damageFactor;

        if(damageEffect != null)
            damageEffect.SetActive(true);

        if(Value <= 0)
        {
            Scr_GameManager.instance.SetStuckTime(0.05f, 0.05f);

            if(destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
