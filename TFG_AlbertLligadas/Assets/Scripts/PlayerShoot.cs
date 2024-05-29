using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootController;
    [SerializeField] private GameObject ball;

    public bool canShoot = false;
    public static PlayerShoot instance;
    public bool shootEnd = true;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && PlayerController.instance.canShoot && shootEnd)
        {
            shootEnd = false;
            PlayerController.instance.anim.SetTrigger("hasShooted");
            Invoke("Shoot", 0.25f);
        }
    }

    private void Shoot()
    {
        AudioManager.instance.PlaySFX(10);
        GameObject newball = Instantiate(ball, shootController.position, shootController.rotation);
        newball.GetComponent<BallController>().SetDirection(PlayerController.instance.facingDirection);
        shootEnd = true;
        
    }

    void hasShooted()
    {
        PlayerController.instance.anim.SetBool("hasShooted", false);
    }
}
