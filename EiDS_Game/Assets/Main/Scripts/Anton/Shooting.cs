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
        GameObject gameObject = Instantiate(bullet, shootingPoint_back.position, transform.rotation);
        Transform parent = GameObject.Find("Disposables").transform;
        gameObject.transform.parent = parent;
    }

    void Shooting_front()
    {
        Quaternion rotationFront = Quaternion.Euler(0f, 0f, 180f);
        GameObject gameObject = Instantiate(bullet, shootingPoint_front.position, rotationFront);
        Transform parent = GameObject.Find("Disposables").transform;
        gameObject.transform.parent = parent;
    }

    void Shooting_left()
    {
        Quaternion rotationLeft = Quaternion.Euler(0f, 0f, 90f);
        GameObject gameObject = Instantiate(bullet, shootingPoint_left.position, rotationLeft);
        Transform parent = GameObject.Find("Disposables").transform;
        gameObject.transform.parent = parent;
    }

    void Shooting_right()
    {
        Quaternion rotationRight = Quaternion.Euler(0f, 0f, -90f);
        GameObject gameObject = Instantiate(bullet, shootingPoint_right.position, rotationRight);
        Transform parent = GameObject.Find("Disposables").transform;
        gameObject.transform.parent = parent;
    }
}
