using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    public Arrow arrowReference;
    public PlayerAttack attackReference;

    void Start()
    {
     
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp_Bow"))
        {
            Destroy(collision.gameObject);
            arrowReference.damageIncrease += 2;
        }

        if (collision.gameObject.CompareTag("PowerUp_Sword"))
        {
            Destroy(collision.gameObject);
            attackReference.damageIncrease += 2;
        }
    }
}
