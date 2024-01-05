using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRightFalse : MonoBehaviour
{
    public GameObject swordRight;

    void Start()
    {
        
    }

    void RightSetFalse()
    {
        swordRight.SetActive(false);
    }
}
