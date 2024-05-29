using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigController : MonoBehaviour
{
    public enum bossStates { shooting, hurt, moving, ended };
    public bossStates currentStates;

    public Transform theBoss;
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShoots;
    private float shootCounter;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;

    public GameObject hitBox;

    [Header("Health")]
    public int health = 5;
    public GameObject explosion, winPlatform;
    private bool isDefeated;
    public float shootSpeedUp;

    private float lifeShoot = .45f;
    private float animStart = .45f;


    // Start is called before the first frame update
    void Start()
    {
        currentStates = bossStates.shooting;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentStates)
        {
            case bossStates.shooting:
                anim.SetBool("isMoving", false);
                shootCounter -= Time.deltaTime;

                if (shootCounter <= 0)
                {
                    anim.SetBool("isShooting", true);
                    shootCounter = timeBetweenShoots;

                    Invoke("ShootOn", animStart);

                    Invoke("ShootOff", lifeShoot);
                }
                break;

            case bossStates.hurt:
                anim.SetBool("isMoving", false);
                if (hurtTime > 0)
                {
                    hurtCounter -= Time.deltaTime;

                    if (hurtCounter <= 0)
                        currentStates = bossStates.moving;

                    if (isDefeated)
                        {
                            theBoss.gameObject.SetActive(false);
                            Instantiate(explosion, theBoss.position, theBoss.rotation);
                            winPlatform.SetActive(true);
                            currentStates = bossStates.ended;
                        }
                }
                break;

            case bossStates.moving:
                anim.SetBool("isMoving", true);
                if (moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        anim.SetBool("isMoving", true);
                        theBoss.localScale = new Vector3(1f, 1f, 1f);

                        moveRight = false;
                        EndMovement();
                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        anim.SetBool("isMoving", true);
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);

                        moveRight = true;
                        EndMovement();
                    }
                }
                break;
        }
    }

    public void TakeHit()
    {
        AudioManager.instance.PlaySFX(12);
        currentStates = bossStates.hurt;
        hurtCounter = hurtTime;

        anim.SetTrigger("Hit");

        health--;

        if (health <= 0)
        {
            isDefeated = true;
        }
        else
        {
            timeBetweenShoots /= shootSpeedUp;
        }
    }

    private void EndMovement()
    {
        currentStates = bossStates.shooting;
        shootCounter = timeBetweenShoots;
        anim.SetTrigger("StopMoving");

        hitBox.SetActive(true);
    }

    private void ShootOn()
    {
        AudioManager.instance.PlaySFX(17);
        var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        newBullet.transform.localScale = theBoss.localScale;
    }

    private void ShootOff()
    {
        anim.SetBool("isShooting", false);
    }
}
