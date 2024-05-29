using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject explodeEffect;
    public GameObject parent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            Destroy(parent);
            Instantiate(explodeEffect, transform.position, transform.rotation);
        }
    }
}
