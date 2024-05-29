using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawHitBox : MonoBehaviour
{
    public CrawController crawCont;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && PlayerController.instance.transform.position.y > transform.position.y)
        {
            crawCont.TakeHit();

            PlayerController.instance.Bounce();

            gameObject.SetActive(false);
        }
    }
}
