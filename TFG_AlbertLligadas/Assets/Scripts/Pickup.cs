using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem, isHeal;

    private bool isCollected;

    public GameObject pickUpEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            if (isGem)
            {
                LevelManager.instance.amatistaCollected++;

                UIController.instance.UpdateGemCount();

                Instantiate(pickUpEffect, transform.position, transform.rotation);

                isCollected = true;
                Destroy(gameObject);
                AudioManager.instance.PlaySFX(0);
            }

            if (isHeal)
            {
                if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
                {
                    PlayerHealthController.instance.HealPlayer();

                    isCollected = true;
                    Destroy(gameObject);
                    AudioManager.instance.PlaySFX(9);
                }
            }
        }
    }
}
