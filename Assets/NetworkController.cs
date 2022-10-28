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
    public static NetworkController instance;
    public GameObject canvas;
    public bool gameStarted;
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
        else
        {
            instance = this;
            if (!Scr_GameManager.instance)
            {
                Scr_GameManager.instance = FindObjectOfType<Scr_GameManager>();
            }
            Scr_GameManager.instance.player = this.gameObject;
        }
        if (!isServer)
        {
            Destroy(canvas);
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
    [Command(requiresAuthority = false)]
    public void CmdSpawn(int index)
    {
        GameObject tospawn = Instantiate(NetworkManager.singleton.spawnPrefabs[index]);
        NetworkServer.Spawn(tospawn, connectionToClient);
    }
    public void spawn(int index)
    {
        CmdSpawn(index);
    }
    public void StartGame()
    {
        gameStarted = true;
        Scr_StartCanvas.instance.Play();
        Destroy(canvas);
    }
    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        if (Input.GetMouseButton(0))
        {
            CmdShoot();
        }
        if (!isServer) return;
        Cursor.visible = !gameStarted;
        Cursor.lockState = gameStarted ? CursorLockMode.Locked : CursorLockMode.None;
    }
   
}
