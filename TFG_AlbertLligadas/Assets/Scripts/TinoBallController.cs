using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinoBallController : MonoBehaviour
{
    public float ballVelocity;

    public GameObject deathEffect;

    private int direction = 1;

    public void SetDirection(int dir)
    {
        direction = dir;
    }
    private void Update()
    {
        transform.Translate(Vector2.right * direction * ballVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DealDamage();
            Instantiate(deathEffect, other.transform.position, other.transform.rotation);
        }
        if (!other.CompareTag("Tino") && !other.CompareTag("HurtBox"))
        {
            Destroy(gameObject);
        }
    }
}
