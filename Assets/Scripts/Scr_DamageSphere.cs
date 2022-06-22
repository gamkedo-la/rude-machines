using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DamageSphere : MonoBehaviour
{
    [SerializeField] private int maxCombo = 2;
    private ParticleSystem particles;
    private Scr_DestroyTimer timer;
    private Scr_DamageOnCollision damageOnCollision;

    private int currentCombo = 0;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        timer = GetComponent<Scr_DestroyTimer>();
        damageOnCollision = GetComponent<Scr_DamageOnCollision>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(currentCombo < maxCombo && other.gameObject.GetComponent<Scr_Projectile>() != null)
        {
            damageOnCollision.DamageRate *= 1.5f;
            timer.delay *= 1.25f;
            transform.localScale += Vector3.one * 0.5f;
            transform.GetChild(0).localScale += Vector3.one * 0.5f;
            particles.Play();
            currentCombo++;
        }
    }
}
