using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoryController : MonoBehaviour
{
    public Animator anim;
    public GameObject smoke;
    public Transform firePoint;

    public float minAttackInterval = 1f;
    public float maxAttackInterval = 3f;
    public float attackDuration = 3f;

    private bool isAttacking = false;
    private GameObject currentSmoke;

    private void Start()
    {
        Invoke("Attack", Random.Range(minAttackInterval, maxAttackInterval));
    }

    private void Update()
    {
        if (!isAttacking && currentSmoke != null)
        {
            Destroy(currentSmoke);
        }
    }

    private void Attack()
    {

        if (gameObject.activeSelf)
        {
            isAttacking = true;
            anim.SetBool("isAttacking", true);

            currentSmoke = Instantiate(smoke, firePoint.position, Quaternion.identity);

            Invoke("EndAttack", attackDuration);
        }
    }

    private void EndAttack()
    {
        isAttacking = false;
        if (currentSmoke != null)
        {
            Destroy(currentSmoke);
            anim.SetBool("isAttacking", false);
        }
        if (gameObject.activeSelf)
        {
            Invoke("Attack", Random.Range(minAttackInterval, maxAttackInterval));
        }
    }
}
