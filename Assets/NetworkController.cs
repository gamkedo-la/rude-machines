using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
public class NetworkController : NetworkBehaviour
{
    public Scr_PlayerController playerController;
    public Scr_HandController handController;
    public Scr_PlayerHealth healthController;
    public Scr_Power powerController;
    public Scr_BarProperty barcontroller;
    public PlayerInput input;
    public Camera cam;
    void Start()
    {
        playerController.enabled = isLocalPlayer;
      //  handController.enabled = isLocalPlayer;
      //  healthController.enabled = isLocalPlayer;
       // powerController.enabled = isLocalPlayer;
       // barcontroller.enabled = isLocalPlayer;
        
        if (!isLocalPlayer)
        {
            Destroy(input);
            Destroy(cam);
        }

    }
    #region Shoot 
    [Command]
    public void CmdShoot()
    {
        RpcShoot();
    }
    [ClientRpc]
    public void RpcShoot()
    {
        
        handController.Shoot();
    }
    #endregion


    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        if (Input.GetMouseButton(0))
        {
            CmdShoot();
        }
    }
   
}
