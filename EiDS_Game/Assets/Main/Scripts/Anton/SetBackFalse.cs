using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBackFalse : MonoBehaviour
{
    public GameObject swordBack;

    void Start()
    {

    }

    void BackSetFalse()
    {
        swordBack.SetActive(false);
    }
}
