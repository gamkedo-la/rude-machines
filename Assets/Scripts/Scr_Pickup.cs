using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Pickup : MonoBehaviour
{
    public float value = 0.2f;
    public float lerpFactor = 2.0f;
    public AudioClip clip;

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            transform.position = Vector3.Lerp(transform.position, coll.transform.position, lerpFactor * Time.deltaTime);

            if (Vector3.Distance(transform.position, coll.transform.position) < 2.0f)
            {
                coll.gameObject.GetComponent<Scr_PlayerController>().AddSlowMo(value);
                coll.gameObject.GetComponent<Scr_PlayerHealth>().Value += 0.1f;

                coll.gameObject.GetComponent<AudioSource>().PlayOneShot(clip);

                Scr_GameManager.instance.SetStuckTime(0.01f);

                Destroy(gameObject);
            }
        }
    }
}
