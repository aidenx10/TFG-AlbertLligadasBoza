using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float ballVelocity;

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
        if(other.CompareTag("Enemy"))
        {
            other.transform.parent.gameObject.SetActive(false);
            Instantiate(deathEffect, other.transform.position, other.transform.rotation);
            Destroy(gameObject);
        }
       else if(other.CompareTag("Tino"))
        {
            TinoController.instance.TakeHit();
            Instantiate(deathEffect, other.transform.position, other.transform.rotation);
            Destroy(gameObject);
       }
    }
}
