using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer sr;

    public Sprite cpOn, cpOff;

    public bool checkUsed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointController.instance.DeactivateCheckpoints();
            sr.sprite = cpOn;

            CheckpointController.instance.SetSpawnPoint(transform.position);
            if (!checkUsed)
            {
                AudioManager.instance.PlaySFX(20);
            }

            if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
            {
                if (!checkUsed)
                {
                    PlayerHealthController.instance.HealPlayer();
                    checkUsed = true;
                }
            }
        }
    }

    public void ResetCheckpoint()
    {
        sr.sprite = cpOff;
    }
}