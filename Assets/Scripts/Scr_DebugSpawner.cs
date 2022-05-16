using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_DebugSpawner : MonoBehaviour
{

    public GameObject defaultEnemy;
    private Scr_PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
      playerController = GetComponent<Scr_PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnDefaultEnemy(InputAction.CallbackContext context)
    {
      if (context.started)
      {
        // GameObject newDefaultEnemy = Instantiate(...);
        float rot_y = playerController.transform.rotation.y;
        Vector3 pos = playerController.transform.position + new Vector3(0,4,8);
        Quaternion rot = Quaternion.Euler(0, rot_y, 0);
        // Debug.Log("BOOP " + rot);
        Instantiate(defaultEnemy, pos, rot);
      }
    }
}
