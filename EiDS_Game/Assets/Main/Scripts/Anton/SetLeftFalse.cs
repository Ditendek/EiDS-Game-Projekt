using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLeftFalse : MonoBehaviour
{
    public GameObject swordLeft;

    void Start()
    {

    }

    void LeftSetFalse()
    {
        swordLeft.SetActive(false);
    }
}
