using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//TODO: power bar system for bullets limitation

public class Scr_HandController : MonoBehaviour
{
    public GameObject hands;
    public Transform[] rotationPoints; //0-base, 1-up, 2-down, 3-left, 4-right
    public GameObject projectile;
    public Transform muzzle;
    public GameObject muzzleParticle;

    private Scr_PlayerController playerController;

    private Vector2 initHandRot;

    void Start()
    {
        playerController = GetComponent<Scr_PlayerController>();
        initHandRot = new Vector2(hands.transform.GetChild(0).localRotation.eulerAngles.x, hands.transform.GetChild(0).localRotation.eulerAngles.y);
    }

    void Update()
    {
        hands.transform.localPosition = Vector3.Lerp(hands.transform.localPosition, hands.transform.up * playerController.move.magnitude * Mathf.Sin(Time.time * 10.0f) / 20.0f, 8.0f * Time.deltaTime);

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

        hands.transform.GetChild(0).localRotation = Quaternion.Slerp(currentRot, targetRot, Time.deltaTime * 8.0f);
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            muzzleParticle.SetActive(true);
            Instantiate(projectile, muzzle.position, muzzle.rotation);
            hands.transform.position -= hands.transform.forward * 0.25f;
        }
    }

    //TODO: Make new muzzle particles for THE SHOTGUN. The remaining power determines the number of bullets.
    public void AltFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            muzzleParticle.SetActive(true);
            int count = Random.Range(8, 13);
            Vector3 rot = muzzle.rotation.eulerAngles;
            for(int i = 0; i < count; i++)
                Instantiate(projectile, muzzle.position, Quaternion.Euler(rot.x + Random.Range(-15.0f, 15.0f), rot.y + Random.Range(-15.0f, 15.0f), rot.z));
        }
    }
}
