using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health, maxHealth;
    private BossMinion bossMinion;
    private BossAI boss;
    private void Start()
    {
        health = maxHealth;
        bossMinion = GetComponent<BossMinion>();
        boss = GetComponent<BossAI>();
    }

    public void TakeDamage(float damage)
    {
        if (boss != null && boss.GetState() == BossAI.State.Spawning) return;
     
        health -= damage;

        if (health <= 0)
        {
            if(bossMinion != null)
            {
                bossMinion.RemoveFromSpawner();
            }
            if(boss==null) Destroy(gameObject);
        }
    }
    public float GetEnemyHealth()
    {
        return health;
    }
}
