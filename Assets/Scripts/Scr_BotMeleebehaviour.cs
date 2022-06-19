using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BotMeleebehaviour : MonoBehaviour
{
    [SerializeField] float xLowerBound, xUpperBound; //Bounds of the platform
    [SerializeField] float zLowerBound, zUpperBound;

    [SerializeField] float randomDirectionRange = 22; //How much randomness do we want when we rotate the bot. MAXIMUM value of 45
    [SerializeField] float movementSpeed = 5;
    Scr_PlayerHealth playerHealth;


    private void Awake()
    {
        playerHealth = FindObjectOfType<Scr_PlayerHealth>();
    }

    private void Update()
    {
        if (playerHealth == null)
        {
            Debug.Log("Out");
        }

        else
        {
            Debug.Log("In");
        }

    }
    void FixedUpdate()
    {

        //Check position FIRST, that way we don't move the bot farther out of bounds
        //If we are out of bounds, find what direction we need the bot to move in to go back in bounds
        bool outOfBounds = false;
        Vector3 towardsCenter = Vector3.zero;
        if (transform.position.x <= xLowerBound)
        {
            towardsCenter += Vector3.right;
            outOfBounds = true;
        }
        else if (transform.position.x >= xUpperBound)
        {
            towardsCenter += Vector3.left;
            outOfBounds = true;
        }

        if (transform.position.z <= zLowerBound)
        {
            towardsCenter += Vector3.forward;
            outOfBounds = true;
        }
        if (transform.position.z >= zUpperBound)
        {
            towardsCenter += Vector3.back;
            outOfBounds = true;
        }


        if (outOfBounds) Bot_Rotate(towardsCenter);

        //Now that we are facing a good direction we can move
        transform.position += movementSpeed * Time.fixedDeltaTime * transform.forward;
    }

    void Bot_Rotate(Vector3 towardsCenter)
    {

        float randomRotate; //Random angle to add
        if (towardsCenter == Vector3.zero)
        {
            randomRotate = Random.Range(0, 360); //Any direction
            towardsCenter = Vector3.forward;
        }
        else randomRotate = Random.Range(-1f * randomDirectionRange, randomDirectionRange); //Restrict to move the bot back in bounds
                                                                                            //  towardsCenter = Vector3.forward;


        Vector3 newDirection = Quaternion.Euler(0, randomRotate, 0) * towardsCenter; //Rotate our towardsCenter vector by the random angle
        transform.rotation = Quaternion.LookRotation(newDirection); //Face the bot in the new direction
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
         playerHealth.Damage(1, gameObject);
         Debug.Log("Player Dead");
            
        }
    }



}
