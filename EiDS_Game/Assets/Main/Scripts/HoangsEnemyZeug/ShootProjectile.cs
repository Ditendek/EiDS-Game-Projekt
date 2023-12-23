using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private Transform pfBullet;
 
    void Awake()
    {
        GetComponent<AimBeetle>().OnShoot += shootProjectile_OnShoot;
    }

            
private void shootProjectile_OnShoot(object sender, AimBeetle.OnShootEventArgs e)
    {
            Transform bulletTransform = Instantiate(pfBullet, e.gunEndPointPosition, Quaternion.identity);
            bulletTransform.GetComponent<BulletShot>().SetUp(e.shootDirection);     
    }
}