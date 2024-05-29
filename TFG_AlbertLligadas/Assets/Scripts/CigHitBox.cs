using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigHitBox : MonoBehaviour
{
    public CigController cigCont;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && PlayerController.instance.transform.position.y > transform.position.y)
        {
            cigCont.TakeHit();

            PlayerController.instance.Bounce();

            gameObject.SetActive(false);
        }
    }
}
