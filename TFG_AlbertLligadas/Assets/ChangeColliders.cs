using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColliders : MonoBehaviour
{
    public static ChangeColliders instance;
    public Collider2D miniCollider;
    public Collider2D regularCollider;

    private void Awake()
    {
        instance = this;
    }
    public void ActivateMiniCollider()
    {
        miniCollider.enabled = true;
        regularCollider.enabled = false;
    }

    public void ActivateRegularCollider()
    {
        miniCollider.enabled = false;
        regularCollider.enabled = true;
    }
}
