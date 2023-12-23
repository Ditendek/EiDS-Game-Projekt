using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool canAttack = true; 
    private Animator anim;
    public float firerate;
    float nextfire;
    private bool isAttacking = false;
    
    void Start()
    {
        anim = GetComponent<Animator>();
       
    }

    
    void Update()
    {
        if (canAttack)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                isAttacking = true;
                ChangeAnimationState("Attack_Back_Bow");
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                isAttacking = true;
                ChangeAnimationState("Attack_Front_Bow");
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                isAttacking = true;
                ChangeAnimationState("Attack_Right_Bow");
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                isAttacking = true;
                ChangeAnimationState("Attack_Left_Bow");
            }
            else
            {
                isAttacking = false;
            }
        }
        else
        {
            isAttacking = false;
        }
    }


    void ChangeAnimationState(string newState)
    {
        if (Time.time > nextfire)
        {
            nextfire = Time.time + firerate;
            anim.Play(newState, 0, 0);
        }

        


       // canAttack = false;
       // StartCoroutine(AttackCooldown());
    }

   /* IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.25f);
        canAttack = true;
    }  */
}
