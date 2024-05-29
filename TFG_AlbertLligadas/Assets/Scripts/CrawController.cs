using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawController : MonoBehaviour
{
    public enum bossStates { attacking, hurt, dizzy, idle, ended };
    public bossStates currentStates;

    public Transform theBoss;
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed, attackSpeedUp;
    public Transform leftPoint, rightPoint;
    public Transform leftPointPosition, rightPointPosition;
    private bool moveRight;

    [Header("Dizzy")]
    public float dizzyTime;
    private float dizzyCounter;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;

    public GameObject hitBox;

    [Header("Health")]
    public int health = 5;
    public GameObject explosion, winPlatform;
    private bool isDefeated;

    private float idleTimer;
    private float idleDuration;

    // Start is called before the first frame update
    void Start()
    {
        currentStates = bossStates.idle;
        SetRandomIdleDuration();
        hitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStates)
        {
            case bossStates.idle:
                idleTimer -= Time.deltaTime;
                if (moveRight)
                {

                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.position = rightPointPosition.position;
                        theBoss.localScale = new Vector3(1f, 1f, 1f);
                        moveRight = false;
                    }
                }
                else
                {
                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.position = leftPointPosition.position;
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        moveRight = true;
                    }
                }
                if (idleTimer <= 0)
                {
                    currentStates = bossStates.attacking;
                    idleTimer = 0;
                }
                break;

            case bossStates.attacking:
                anim.SetBool("isAttacking", true);
                hitBox.SetActive(false);
                if (moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        anim.SetBool("isAttacking", true);
                        EndMovement();
                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        anim.SetBool("isAttacking", true);
                        EndMovement();
                    }
                }
                break;

            case bossStates.dizzy:
                if (dizzyTime > 0)
                {
                    dizzyCounter -= Time.deltaTime;

                    if (dizzyCounter <= 0)
                    {
                        currentStates = bossStates.idle;
                        SetRandomIdleDuration();
                        anim.SetBool("isDizzy", false);
                        anim.SetBool("isHurted", false);
                    }
                }
                break;
            case bossStates.hurt:
                if (hurtTime > 0)
                {
                    hurtCounter -= Time.deltaTime;

                    if (hurtCounter <= 0)
                    {
                        currentStates = bossStates.idle;
                        SetRandomIdleDuration();
                        anim.SetBool("isHurted", false);
                    }
                    if (isDefeated)
                    {
                        theBoss.gameObject.SetActive(false);
                        Instantiate(explosion, theBoss.position, theBoss.rotation);
                        winPlatform.SetActive(true);
                        currentStates = bossStates.ended;
                    }
                }
                break;
        }
    }

    public void TakeHit()
    {
        AudioManager.instance.PlaySFX(13);
        currentStates = bossStates.hurt;
        hurtCounter = hurtTime;

        anim.SetBool("isHurted", true);

        health--;

        if (health <= 0)
        {
            isDefeated = true;
        }

        moveSpeed += attackSpeedUp;
    }

    private void EndMovement()
    {
        AudioManager.instance.PlaySFX(18);
        currentStates = bossStates.dizzy;
        anim.SetBool("isAttacking", false);
        anim.SetBool("isDizzy", true);

        dizzyCounter = dizzyTime;

        hitBox.SetActive(true);
    }

    private void SetRandomIdleDuration()
    {
        idleDuration = Random.Range(3f, 6f);
        idleTimer = idleDuration;
    }
}
