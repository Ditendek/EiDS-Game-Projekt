using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBackFalse : MonoBehaviour
{
    public GameObject swordBack;
    public bool back;

    void Start()
    {

    }

    void BackSetFalse()
    {
        swordBack.SetActive(false);
    }

    void number1()
    {
        back = true;
    }

    void number2()
    {
        back = false;
    }
}
