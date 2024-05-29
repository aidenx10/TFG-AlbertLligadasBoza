using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTimeEffect : MonoBehaviour
{
    public float lifeTime;
    public GameObject deathEffect;

    private void Start()
    {
        Invoke("DestroyObject", lifeTime);
    }

    private void DestroyObject()
    { 
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
