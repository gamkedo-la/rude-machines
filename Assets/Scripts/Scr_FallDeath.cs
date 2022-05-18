using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_FallDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10.0f){
            Debug.Log("Fell too far");
            Scr_PlayerController playerScript = gameObject.GetComponent<Scr_PlayerController>();
            playerScript.Die();
        }
    }
}
