using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;

    private int damageLevel = 0;
    private float damageMutiplier = 1f;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    void TakeDamage(int damage)
    {
        DungeonBuilder dungeon = GameObject.Find("Dungeon").GetComponent<DungeonBuilder>();

        if(dungeon.level != damageLevel) {
            damageLevel = dungeon.level;

            damageMutiplier = (damageLevel - 1) * 0.25f + 1;
        }

        currentHealth -= (int) (damage * damageMutiplier);
        healthbar.SetHealth(currentHealth);
        
        if (currentHealth < 1)
        {
            Death();
        }
    }

    void HealLife()
    {
        currentHealth += 15;
        healthbar.SetHealth(currentHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            TakeDamage(10);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DamageTrigger"))
        {
            TakeDamage(10);
        }
        if (collision.gameObject.CompareTag("Heal"))
        {
            if (currentHealth < maxHealth) 
            {
                Destroy(collision.gameObject);
                HealLife();
            }
        }
         
    }

    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
