using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    public int bowcounter;
    public int swordcounter;

    public Arrow arrowReference;
    public PlayerAttack attackReference;

    public Text bowText;
    public Text swordText;


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp_Bow"))
        {
            Destroy(collision.gameObject);
            bowcounter += 1;
            arrowReference.damageIncrease += 2;
            bowText.text = "+" + bowcounter;
        }

        if (collision.gameObject.CompareTag("PowerUp_Sword"))
        {
            Destroy(collision.gameObject);
            swordcounter += 1;
            attackReference.damageIncrease += 2;
            swordText.text = "+" + swordcounter;
        }
    }
}
