using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : AttackMode
{

    Animator animator;
    public float dashForce;
    private Rigidbody2D rb;
   // private bool collidesPlayer;

    private void Start()
    {
        animator = transform.Find("SlimeGFX").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
      //  collidesPlayer = false;
    }
    public override void Attack()
    {
        animator.SetTrigger("Attack");
       Vector3 force = (PlayerMovement.PlayerPosition - transform.position).normalized * dashForce;   
       rb.AddForce(force);
    }
    /*
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collidesPlayer = true;
         //   Debug.Log("HIT");
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collidesPlayer = false;
        }
    }*/
}
