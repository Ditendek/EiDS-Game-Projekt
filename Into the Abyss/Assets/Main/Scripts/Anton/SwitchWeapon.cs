using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchWeapon : MonoBehaviour
{
    public Image image1;
    public Image image2;

    public bool weaponswitch = true;

    void Start()
    {
        image1.enabled = true;
        image2.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            image1.enabled = !image1.enabled;
            image2.enabled = !image2.enabled;
            weaponswitch = !weaponswitch;
        }
    }
}
