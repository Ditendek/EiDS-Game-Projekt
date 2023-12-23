using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool canAttack = true; 
    private Animator anim;
    public float firerate;
    float nextfire;
    
    void Start()
    {
        anim = GetComponent<Animator>();
       
    }

    
    void Update()
    {
        if (canAttack)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ChangeAnimationState("Attack_Back_Bow");
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ChangeAnimationState("Attack_Front_Bow");
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeAnimationState("Attack_Right_Bow");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeAnimationState("Attack_Left_Bow");
            }
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
