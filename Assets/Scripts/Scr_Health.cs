using UnityEngine;

public class Scr_Health : Scr_BarProperty, Scr_IDamageable
{
    [Space]
    public float damageFactor = 1.0f;
    public GameObject damageEffect = null;
    public AudioClip[] damageSFX;
    [Space]
    public GameObject destroyEffect = null;
    public Vector3 destroyEffectScale = Vector3.one;
    public AudioClip destroySFX;
    [Space]
    public Scr_Detect detector = null;
    public float pauseDetectorForTime = 0.0f;

    private AudioSource audSrc;

    void Start()
    {
        audSrc = GetComponent<AudioSource>();
    }

    virtual public void Damage(float value, GameObject instigator)
    {
        bool isPlayer = instigator != null && instigator.tag == "Player";
        
        if(detector != null && isPlayer)
        {
            detector.detectedTarget = instigator.transform;
            detector.SetTimer(pauseDetectorForTime);
        }

        Value -= value * damageFactor;

        if(damageEffect != null)
            damageEffect.SetActive(true);

        if(Value <= 0.0f)
        {
            if(isPlayer) Scr_GameManager.instance.SetStuckTime(0.08f, 0.04f);
            if(destroyEffect != null)
            {
                GameObject newDestroyEffect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
                newDestroyEffect.transform.localScale = destroyEffectScale;
                newDestroyEffect.GetComponent<AudioSource>().PlayOneShot(destroySFX);
            }
            Destroy(gameObject);
        }
        else
        {
            audSrc.PlayOneShot(damageSFX[Random.Range(0, damageSFX.Length)]);
        }
    }
}
