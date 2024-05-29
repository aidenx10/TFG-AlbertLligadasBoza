using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool falling = false;
    private Rigidbody2D rb;

    private void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !falling)
        {
            falling = true;
            Invoke("Fall", 0.5f);
            Invoke("Respawn", 5f); 
        }
    }

    private void Fall()
    {
        rb.isKinematic = false;
    }

    private void Respawn()
    {
        falling = false;
        transform.position = originalPosition;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }
}
