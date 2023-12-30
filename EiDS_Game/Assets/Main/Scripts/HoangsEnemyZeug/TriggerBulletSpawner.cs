using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerBulletSpawner : MonoBehaviour
{

    public BulletSpawner bulletSpawner;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    IEnumerator WaitAndTriggerAnimation()
    {
        // Wait for 2.5 seconds
        yield return new WaitForSeconds(2.5f);

        animator.SetTrigger("StartJumping");
    }
    //Animationevents

    private void EnableBullets()
    {
        bulletSpawner.enabled = true;
        StartCoroutine(WaitAndTriggerAnimation());
    }

    private void DisableBullets()
    {
        bulletSpawner.enabled = false;
    }
}
