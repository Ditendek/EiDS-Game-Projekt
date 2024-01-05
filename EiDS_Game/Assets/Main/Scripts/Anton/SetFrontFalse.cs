using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFrontFalse : MonoBehaviour
{
    public GameObject swordFront;

    void Start()
    {

    }

    void FrontSetFalse()
    {
        swordFront.SetActive(false);
    }
}