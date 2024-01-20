using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRightFalse : MonoBehaviour
{
    public GameObject swordRight;
    public bool right = false;

    void Start()
    {
        
    }

    void RightSetFalse()
    {
        swordRight.SetActive(false);
    }

    void number1()
    {
        right = true;
    }

    void number2()
    {
        right = false;
    }
}
