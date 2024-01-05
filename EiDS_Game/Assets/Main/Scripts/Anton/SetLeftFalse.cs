using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLeftFalse : MonoBehaviour
{
    public GameObject swordLeft;
    public bool left;

    void Start()
    {

    }

    void LeftSetFalse()
    {
        swordLeft.SetActive(false);
    }

    void number1()
    {
        left = true;
    }

    void number2()
    {
        left = false;
    }
}
