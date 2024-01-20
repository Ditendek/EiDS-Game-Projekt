using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFrontFalse : MonoBehaviour
{
    public GameObject swordFront;
    public bool front;

    void Start()
    {

    }

    void FrontSetFalse()
    {
        swordFront.SetActive(false);
    }
    void number1()
    {
        front = true;
    }

    void number2()
    {
        front = false;
    }

}