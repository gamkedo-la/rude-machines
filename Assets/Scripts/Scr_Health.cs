using UnityEngine;

public class Scr_Health : Scr_BarProperty, Scr_IDamageable
{
    [Space]
    public float damageFactor = 1.0f;
    public GameObject damageEffect = null;
    public GameObject destroyEffect = null;
    public Vector3 destroyEffectScale = Vector3.one;

    virtual public void Damage(float value, GameObject instigator)
    {
        Value -= value * damageFactor;

        if(damageEffect != null)
            damageEffect.SetActive(true);

        if(Value <= 0)
        {
            if(instigator.tag == "Player")
            {
                Scr_GameManager.instance.SetStuckTime(0.08f, 0.04f);
            }

            if(destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity).transform.localScale = destroyEffectScale;
            }
            Destroy(gameObject);
        }
    }
}
