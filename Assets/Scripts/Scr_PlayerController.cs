using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scr_PlayerController : MonoBehaviour
{
    public float movementFactor = 1.0f;
    public float rotationFactor = 1.0f;
    public bool yRotationInvert = true;
    [Header("Jump")]
    public float jumpStartVelocity = 1.0f;
    public float jumpUpAcceleration = 1.0f;
    public float jumpUpThrust = 600.0f;
    public float jumpDownForce = 10.0f;

    private Rigidbody rb;
    private Camera cam;

    [HideInInspector] public Vector2 move = Vector2.zero;
    [HideInInspector] public Vector2 rotation = Vector2.zero;

    private float jumpAcceleration = 0.0f;
    private float secondsWhenRoundStarted = 0.0f; // to subtract menu time from play time when dying
    private float secondsWhenDied = 0.0f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = transform.GetChild(0).GetComponent<Camera>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        secondsWhenRoundStarted = Time.timeSinceLevelLoad; 
        float bestTime = PlayerPrefs.GetFloat("bestTime", 0.0f);
        Debug.Log("Best survival time score is "+bestTime);
    }

    public void Die(){
        secondsWhenDied = Time.timeSinceLevelLoad;
        float timeScore = secondsWhenDied - secondsWhenRoundStarted;
        Debug.Log("Time score: "+timeScore);
        float bestTime = PlayerPrefs.GetFloat("bestTime", 0.0f);
        if(timeScore > bestTime){
            Debug.Log("New best time");
            PlayerPrefs.SetFloat("bestTime", timeScore);
        } else {
            Debug.Log("This was not the best time score");
        }

        // TODO: Make a timer before restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void FirstPersonMovement()
    {
        Vector3 movement = (transform.forward * move.y) + (transform.right * move.x);
        movement *= movementFactor;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    void FirstPersonRotation()
    {
        Vector3 rot = cam.transform.localRotation.eulerAngles;
        rot.x += (yRotationInvert ? -1.0f : 1.0f) * rotation.y * rotationFactor;
        rot.y = rot.z = 0.0f;
        cam.transform.localRotation = Quaternion.Euler(rot);

        rot = transform.localRotation.eulerAngles;
        rot.y += rotation.x * rotationFactor;
        transform.localRotation = Quaternion.Euler(rot);
    }

    void JumpProcess()
    {
        Vector2 vel = rb.velocity;
        if (jumpAcceleration > 0.0f)
        {
            vel.y += jumpAcceleration * Time.fixedDeltaTime;
            jumpAcceleration -= Time.fixedDeltaTime;
        }
        vel.y -= jumpDownForce * Time.fixedDeltaTime;
        rb.velocity = vel;
    }

    void Update()
    {
        FirstPersonRotation();
    }

    void FixedUpdate()
    {
        FirstPersonMovement();
        JumpProcess();
    }

    public void Move(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStartVelocity);
            jumpAcceleration = jumpUpAcceleration;
            rb.AddForce(transform.up * jumpUpThrust, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, transform.forward, -transform.up, Quaternion.identity, 0.6f);
        foreach (var hit in hits)
            if (hit.collider.gameObject != gameObject)
                return true;
        return false;
    }
}
