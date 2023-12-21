using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform shootingPoint_back;
    public Transform shootingPoint_front;
    public Transform shootingPoint_left;
    public Transform shootingPoint_right;
    public GameObject bullet;

    void Update()
    {
        
    }

    void Shooting_back()
    {
        Instantiate(bullet, shootingPoint_back.position, transform.rotation);
    }

    void Shooting_front()
    {
        Quaternion rotationFront = Quaternion.Euler(0f, 0f, 180f);
        Instantiate(bullet, shootingPoint_front.position, rotationFront);
    }

    void Shooting_left()
    {
        Quaternion rotationLeft = Quaternion.Euler(0f, 0f, 90f);
        Instantiate(bullet, shootingPoint_left.position, rotationLeft);
    }

    void Shooting_right()
    {
        Quaternion rotationRight = Quaternion.Euler(0f, 0f, -90f);
        Instantiate(bullet, shootingPoint_right.position, rotationRight);
    }
}
