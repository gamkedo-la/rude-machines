using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Debug.Log("Time alive: "+Time.timeSinceLevelLoad);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
