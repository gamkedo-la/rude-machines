using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [Space]
    public AudioSource footstepsAudSrc;
    [Space]
    [SerializeField] private Scr_BarProperty slowMoBar;
    [SerializeField] private float slowMoConsumptionRate = 1.0f;

    private Rigidbody rb;
    private Camera cam;

    [HideInInspector] public Vector2 move = Vector2.zero;
    [HideInInspector] public Vector2 rotation = Vector2.zero;

    private float jumpAcceleration = 0.0f;

    private bool slowMoControl = false;

    public void AddSlowMo(float value)
    {
        slowMoBar.Value += value;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = transform.GetChild(0).GetComponent<Camera>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FirstPersonMovement(ref Vector3 velocity)
    {
        Vector3 movement = (transform.forward * move.y) + (transform.right * move.x);
        movement *= movementFactor;

        velocity.x = movement.x * (Time.timeScale < 1.0f ? 5.0f : 1.0f);
        velocity.z = movement.z * (Time.timeScale < 1.0f ? 5.0f : 1.0f);

        footstepsAudSrc.enabled = Mathf.Abs(move.x) + Mathf.Abs(move.y) > 0.5f && IsGrounded();
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

    void JumpProcess(ref Vector3 velocity)
    {
        if (jumpAcceleration > 0.0f)
        {
            velocity.y += jumpAcceleration * Time.fixedDeltaTime * (Time.timeScale < 1.0f ? 10.0f : 1.0f);
            jumpAcceleration -= Time.fixedDeltaTime * (Time.timeScale < 1.0f ? 5.0f : 1.0f);
        }
        velocity.y -= jumpDownForce * Time.fixedDeltaTime * (Time.timeScale < 1.0f ? 5.0f : 1.0f);
    }

    void Update()
    {
        if(Time.timeScale <= 0.0f) return;
        FirstPersonRotation();

        if (slowMoControl)
        {
            float slowMoStep = Time.unscaledDeltaTime * 5.0f;
            if (slowMoBar.Value >= slowMoStep * slowMoConsumptionRate
            && Scr_GameManager.instance.SlowMo < slowMoStep)
            {
                Scr_GameManager.instance.SlowMo += slowMoStep;
                slowMoBar.Value -= slowMoStep * slowMoConsumptionRate;
            }
        }
    }

    void FixedUpdate()
    {
        if(Time.timeScale <= 0.0f) return;

        Vector3 velocity = rb.velocity;
        FirstPersonMovement(ref velocity);
        JumpProcess(ref velocity);
        rb.velocity = velocity;
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

    public void SlowMo(InputAction.CallbackContext context)
    {
        if (context.started) slowMoControl = true;
        else if(context.canceled) slowMoControl = false;
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
