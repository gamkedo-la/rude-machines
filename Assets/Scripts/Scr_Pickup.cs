using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Pickup : MonoBehaviour
{
    public float value = 0.2f;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Scr_PlayerController>().AddSlowMo(value);
            Scr_GameManager.instance.SetStuckTime(0.01f);
            Destroy(gameObject);
        }
    }
}
