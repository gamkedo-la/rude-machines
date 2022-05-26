using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    [SerializeField] float proximity = 5f;
    float distanceToTarget = Mathf.Infinity;

    private void Update()
    {
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

        if (distanceToTarget <= proximity)
        {
            StartCoroutine(SelfDest());
        }

     }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, proximity);
    }

    IEnumerator SelfDest()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
        Debug.Log("Destroyed");
    }


}
