using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    private Animator anim;

    public float bounceForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioManager.instance.PlaySFX(19);
            PlayerController.instance.rb.velocity = new Vector2(PlayerController.instance.rb.velocity.x, bounceForce);
            anim.SetTrigger("Bounce");
        }
    }
}
