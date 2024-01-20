using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
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
