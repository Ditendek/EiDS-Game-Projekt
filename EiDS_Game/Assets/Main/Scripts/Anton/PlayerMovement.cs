using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 moveDirection;

    //Animations
    private Animator anim;
    private string lastDirection = "front";
    public static string currentState;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
         float moveX = Input.GetAxisRaw("Horizontal");
         float moveY = Input.GetAxisRaw("Vertical");

         moveDirection = new Vector2(moveX, moveY).normalized;

         if (moveX > 0f)
         {
             ChangeAnimationState("Player_Running_Right");
             lastDirection = "right";
         }
         else if (moveX < 0f)
         {
             ChangeAnimationState("Player_Running_Left");
             lastDirection = "left";
         }
         else if (moveY > 0f)
         {
             ChangeAnimationState("Player_Running_Back");
             lastDirection = "back";
         }
         else if (moveY < 0f)
         {
             ChangeAnimationState("Player_Running_Front");
             lastDirection = "front";
         }

         if (moveX == 0f && moveY == 0f)
         {
             if (lastDirection == "right")
             {
                 ChangeAnimationState("Player_Idle_Right");
             }
             else if (lastDirection == "left")
             {
                 ChangeAnimationState("Player_Idle_Left");
             }
             else if (lastDirection == "back")
             {
                 ChangeAnimationState("Player_Idle_Back");
             }
             else
             {
                 ChangeAnimationState("Player_Idle_Front");
             }
         }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void ChangeAnimationState(string newState)
    {
       if (currentState == newState) return;

       anim.Play(newState);
       
       currentState = newState;
    }
}
