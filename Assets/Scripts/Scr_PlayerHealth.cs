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
    [Space]
    public GameObject directionalDamageIndicator;
    public GameObject directionalDamageIndicatorGroup;

    private AudioSource audSrc;
    private float defaultFOV = 72.0f;

    void Start()
    {
        audSrc = GetComponent<AudioSource>();
    }

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

        if (instigator != null)
        {
            float angle = Vector2.Angle(new Vector2(instigator.transform.position.x, instigator.transform.position.z), new Vector2(transform.position.x, transform.position.z));
            GameObject newDirectionalDamageIndicator = Instantiate(directionalDamageIndicator, Vector3.zero, Quaternion.identity);
            newDirectionalDamageIndicator.transform.parent = directionalDamageIndicatorGroup.transform;
            newDirectionalDamageIndicator.transform.localPosition = Vector3.zero;
            newDirectionalDamageIndicator.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
        }

        if (Value <= 0.0f)
        {
            if(destroyEffect != null)
            {
                GameObject newDestroyEffect = Instantiate(destroyEffect, transform.position + (transform.forward * 5.0f), Quaternion.identity);
                newDestroyEffect.transform.localScale = destroyEffectScale;
                newDestroyEffect.GetComponent<AudioSource>().PlayOneShot(destroySFX);
            }
            Scr_GameManager.instance.Die();
        }
        else
        {
            audSrc.PlayOneShot(damageSFX[Random.Range(0, damageSFX.Length)]);
            audSrc.pitch = Random.Range(2.4f, 2.6f);
        }
    }
    
}
