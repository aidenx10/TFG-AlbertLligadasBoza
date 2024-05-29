using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer sr;

    public GameObject deathEffect;

    public string menuScene;

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        currentHealth = maxHealth;

        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter <= 0)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            }
        }
    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {
            currentHealth--;
            PlayerController.instance.anim.SetTrigger("Hurt");

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                Instantiate(deathEffect, PlayerController.instance.transform.position, PlayerController.instance.transform.rotation);

                AudioManager.instance.PlaySFX(7);
                gameObject.SetActive(false);
                Invoke("GoToScene", 1f);


            }
            else
            {
                invincibleCounter = invincibleLength;
                PlayerController.instance.canShoot = false;
                PlayerController.instance.anim.SetBool("canShoot", false);
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, .5f);

                PlayerController.instance.Knockback();
                AudioManager.instance.PlaySFX(3);

                if (currentHealth == 1)
                {
                    PlayerController.instance.anim.SetBool("isMini", true);
                    ChangeColliders.instance.ActivateMiniCollider();
                }
            }

            UIController.instance.UpdateHealthDisplay();
        }
    }

    public void HealPlayer()
    {
        currentHealth++;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        PlayerController.instance.anim.SetBool("isMini", false);
        ChangeColliders.instance.ActivateRegularCollider();
        UIController.instance.UpdateHealthDisplay();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       if (other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }

    public void GoToScene()
    {
        SceneManager.LoadScene(menuScene);
    }
}
