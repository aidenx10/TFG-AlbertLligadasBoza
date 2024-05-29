using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallerPickUp : MonoBehaviour
{
    public static BallerPickUp instance;
    public GameObject pickUpEffect;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(pickUpEffect, transform.position, transform.rotation);
            PlayerController.instance.canShoot = true;

            AudioManager.instance.PlaySFX(4);
            PlayerController.instance.anim.SetBool("canShoot", true);

            PlayerHealthController.instance.HealPlayer();

            Destroy(gameObject);
        }
    }
}
