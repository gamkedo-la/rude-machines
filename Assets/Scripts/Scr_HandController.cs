using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_HandController : MonoBehaviour
{
    [SerializeField] private float firePowerCost = 0.25f;
    [SerializeField] private float fireSpreadAngle = 2.0f;
    [SerializeField] private float recoilFactor = 0.1f;
    [SerializeField] private float burstSpreadAngle = 20.0f;
    [SerializeField] private float burstDelayBetweenShots = 0.05f;
    [Space]
    public GameObject hands;
    public Transform[] rotationPoints; //0-base, 1-up, 2-down, 3-left, 4-right
    public GameObject projectile;
    public Transform muzzle;
    public GameObject muzzleParticle;

    private Scr_PlayerController playerController;
    private Scr_Power power;

    private Vector2 initHandRot;

    private bool burst = false;
    private float burstTimer = 0.0f;

    void Start()
    {
        playerController = GetComponent<Scr_PlayerController>();
        power = GetComponent<Scr_Power>();
        power.Value = 1.0f;

        initHandRot = new Vector2(hands.transform.GetChild(0).localRotation.eulerAngles.x, hands.transform.GetChild(0).localRotation.eulerAngles.y);
    }

    void Update()
    {
        UpdatePosition();
        UpdateRotation();
        BurstSequence();
    }

    void UpdatePosition()
    {
        hands.transform.localPosition = Vector3.Lerp(hands.transform.localPosition, hands.transform.up * playerController.move.magnitude * Mathf.Sin(Time.time * 10.0f) / 20.0f, 12.0f * Time.deltaTime);
    }

    void UpdateRotation()
    {
        Quaternion currentRot = hands.transform.GetChild(0).localRotation;

        hands.transform.GetChild(0).LookAt(rotationPoints[0]);
        Quaternion baseRot = hands.transform.GetChild(0).localRotation;
        hands.transform.GetChild(0).LookAt(playerController.move.y > 0 ? rotationPoints[1] : rotationPoints[2]);
        Quaternion verticalRot = hands.transform.GetChild(0).localRotation;
        hands.transform.GetChild(0).LookAt(playerController.move.x > 0 ? rotationPoints[3] : rotationPoints[4]);
        Quaternion horizontalRot = hands.transform.GetChild(0).localRotation;

        Quaternion targetRot = baseRot;
        targetRot = Quaternion.Slerp(targetRot, verticalRot, Mathf.Abs(playerController.move.y));
        targetRot = Quaternion.Slerp(targetRot, horizontalRot, Mathf.Abs(playerController.move.x));

        hands.transform.GetChild(0).localRotation = Quaternion.Slerp(currentRot, targetRot, Time.deltaTime * 12.0f);
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (power && power.Value > firePowerCost && !burst && context.started)
        {
            muzzleParticle.SetActive(true);
            Vector3 rot = muzzle.rotation.eulerAngles;
            float spreadAngle = fireSpreadAngle * (1.0f - power.Value);
            Instantiate(projectile, muzzle.position, Quaternion.Euler(rot.x + Random.Range(-spreadAngle, spreadAngle), rot.y + Random.Range(-spreadAngle, spreadAngle), rot.z));
            hands.transform.position -= hands.transform.forward * recoilFactor;
            power.Value -= firePowerCost;
        }
    }

    public void AltFire(InputAction.CallbackContext context)
    {
        if (power && power.Value > firePowerCost * 2.5f && context.started)
        {
            burst = true;
            burstTimer = 0.0f;
        }
    }

    void BurstSequence()
    {
        if (burst)
        {
            if (burstTimer <= 0.0f)
            {
                if (power.Value > firePowerCost / 1.5f)
                {
                    muzzleParticle.SetActive(true);

                    Vector3 rot = muzzle.rotation.eulerAngles;
                    float spreadAngle = burstSpreadAngle * (1.0f - power.Value);
                    Instantiate(projectile, muzzle.position, Quaternion.Euler(rot.x + Random.Range(-spreadAngle, spreadAngle), rot.y + Random.Range(-spreadAngle, spreadAngle), rot.z));

                    hands.transform.position -= hands.transform.forward * recoilFactor;
                    power.Value -= firePowerCost / 1.5f;

                    burstTimer = burstDelayBetweenShots;
                }
                else
                {
                    burst = false;
                    burstTimer = 0.0f;
                }
            }
            else
            {
                burstTimer -= Time.deltaTime;
            }
        }
    }
}
