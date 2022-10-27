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
    public Transform[] hand;
    public Transform[] muzzle;
    public GameObject[] muzzleParticle;
    [Space]
    public Transform[] rotationPoints; //0-base, 1-up, 2-down, 3-left, 4-right
    public GameObject singleProjectile;
    public GameObject burstProjectile;
    
    private Scr_PlayerController playerController;
    private Scr_Power power;

    private Vector3[] initHandPos = { Vector2.zero, Vector2.zero };
    private Vector2[] initHandRot = { Vector2.zero, Vector2.zero };

    private bool burst = false;
    private float burstTimer = 0.0f;

    int currentAttackingHand = 0;

    void Start()
    {
        playerController = GetComponent<Scr_PlayerController>();
        power = GetComponent<Scr_Power>();
        power.Value = 1.0f;

        for(int i = 0; i < 2; i++)
        {
            initHandPos[i] = new Vector3(hand[i].localPosition.x, hand[i].localPosition.y, hand[i].localPosition.z);
            initHandRot[i] = new Vector2(hand[i].localRotation.eulerAngles.x, hand[i].localRotation.eulerAngles.y);
        }
    }

    void Update()
    {
        if(Time.timeScale <= 0.0f || Scr_State.block) return;
        UpdatePosition();
        UpdateRotation();
        BurstSequence();
    }

    void UpdatePosition()
    {
        for(int i = 0; i < 2; i++)
        {
            hand[i].localPosition = Vector3.Lerp(hand[i].localPosition, initHandPos[i] + (-hand[i].forward * playerController.move.magnitude * Mathf.Sin(Time.time * 10.0f) / 20.0f), 12.0f * Time.unscaledDeltaTime);
        }
    }

    void UpdateRotation()
    {
        for(int i = 0; i < 2; i++)
        {
            Quaternion currentRot = hand[i].localRotation;

            hand[i].LookAt(rotationPoints[0]);
            Quaternion baseRot = hand[i].localRotation;
            hand[i].LookAt(playerController.move.y > 0 ? rotationPoints[1] : rotationPoints[2]);
            Quaternion verticalRot = hand[i].localRotation;
            hand[i].LookAt(playerController.move.x > 0 ? rotationPoints[3] : rotationPoints[4]);
            Quaternion horizontalRot = hand[i].localRotation;

            Quaternion targetRot = baseRot;
            targetRot = Quaternion.Slerp(targetRot, verticalRot, Mathf.Abs(playerController.move.y));
            targetRot = Quaternion.Slerp(targetRot, horizontalRot, Mathf.Abs(playerController.move.x));

            hand[i].localRotation = Quaternion.Slerp(currentRot, targetRot, Time.unscaledDeltaTime * 12.0f);
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (power && power.Value > firePowerCost && !burst && context.started && !Scr_State.block)
        {
            currentAttackingHand = currentAttackingHand == 0 ? 1 : 0;

            muzzleParticle[currentAttackingHand].SetActive(true);

            Vector3 rot = muzzle[currentAttackingHand].rotation.eulerAngles;
            float spreadAngle = fireSpreadAngle * (1.0f - power.Value);
            GameObject newProjectile = Instantiate(singleProjectile, muzzle[currentAttackingHand].position, Quaternion.Euler(rot.x + Random.Range(-spreadAngle, spreadAngle), rot.y + Random.Range(-spreadAngle, spreadAngle), rot.z));
            newProjectile.GetComponent<Scr_Projectile>().SetGameobject(gameObject);
            newProjectile.GetComponent<AudioSource>().pitch = Random.Range(1.6f, 1.8f);

            hand[currentAttackingHand].position -= hand[currentAttackingHand].transform.forward * recoilFactor;

            power.Value -= firePowerCost;
        }
    }

    public void AltFire(InputAction.CallbackContext context)
    {
        if (power && power.Value > firePowerCost * 2.5f && context.started && !Scr_State.block)
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
                    currentAttackingHand = currentAttackingHand == 0 ? 1 : 0;

                    muzzleParticle[currentAttackingHand].SetActive(true);

                    Vector3 rot = muzzle[currentAttackingHand].rotation.eulerAngles;
                    float spreadAngle = burstSpreadAngle * (1.0f - power.Value);
                    GameObject newProjectile = Instantiate(burstProjectile, muzzle[currentAttackingHand].position, Quaternion.Euler(rot.x + Random.Range(-spreadAngle, spreadAngle), rot.y + Random.Range(-spreadAngle, spreadAngle), rot.z));
                    newProjectile.GetComponent<Scr_Projectile>().SetGameobject(gameObject);

                    hand[currentAttackingHand].position -= hand[currentAttackingHand].forward * recoilFactor;
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
                burstTimer -= Time.unscaledDeltaTime;
            }
        }
    }
}
