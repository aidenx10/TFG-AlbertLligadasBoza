using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinoBombController : MonoBehaviour
{
    public GameObject explosion;

    // Update is called once per frame
    void Update()
    {
        Invoke("BombExplosion", .75f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            BombExplosion();
            PlayerHealthController.instance.DealDamage();
        }
    }

    public void BombExplosion()
    {
        AudioManager.instance.PlaySFX(16);
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
