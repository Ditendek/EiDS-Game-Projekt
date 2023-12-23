using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health, maxHealth;
    private BossMinion bossMinion;
    private void Start()
    {
        health = maxHealth;
        bossMinion = GetComponent<BossMinion>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if(bossMinion != null)
            {
                bossMinion.RemoveFromSpawner();
            }
            Destroy(gameObject);
        }
    }
}
