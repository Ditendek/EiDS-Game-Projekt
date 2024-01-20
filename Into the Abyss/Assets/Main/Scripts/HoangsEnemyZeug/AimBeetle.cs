using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class AimBeetle : AttackMode
{
 
    private Transform aimTransform;
    private Transform gunEndPointTransform;
    Vector3 aimDirection;
    public float knockBackSpeed;
    Rigidbody2D rb;

  
 

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
   //     aimAnimator = aimTransform.GetComponent<Animator>();
        gunEndPointTransform = aimTransform.Find("GunEndPointPosition");

    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Aim()
    {
        HandleAiming();
    }

    private void HandleAiming()
    {
        Vector3 targetPosition = PlayerMovement.PlayerPosition;

        aimDirection = (targetPosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    public override void Attack() 
    {
        HandleAiming();
        // EVENT can also be used in another script for particle efects
        OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = gunEndPointTransform.position,
                shootDirection = (PlayerMovement.PlayerPosition - gunEndPointTransform.position).normalized,
            });
            //KLAPPT
            //transform.position = transform.position + (transform.position - PlayerMovement.PlayerPosition).normalized * Time.deltaTime * knockBackSpeed;
           Vector2 dir = ((Vector2) transform.position - (Vector2)PlayerMovement.PlayerPosition).normalized;
           rb.AddForce(dir * knockBackSpeed);
       
        

     }      

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs {
        public Vector3 gunEndPointPosition;
        public Vector3 shootDirection;
    };

   
}
