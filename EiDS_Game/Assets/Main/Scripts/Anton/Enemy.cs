using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float health, maxHealth;
    private float itemDropPropability = 0.5f;
    private BossMinion bossMinion;
    private BossAI boss;
    SpriteRenderer spriteRenderer;
    Color damageColor = new Color(1f, 0.5f, 0.5f, 1f);

    private void Start() {
        health = maxHealth;
        bossMinion = GetComponent<BossMinion>();
        boss = GetComponent<BossAI>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void TakeDamage(float damage) {
        if(boss != null && boss.GetState() == BossAI.State.Spawning)
            return;


        if(spriteRenderer != null)
            StartCoroutine(DamageAnimation());
        health -= damage;

        if(health <= 0) {
            if(bossMinion != null) {
                bossMinion.RemoveFromSpawner();
            }
            if(bossMinion == null && boss == null && UnityEngine.Random.Range(0f, 1f) <= itemDropPropability) {
                print("drop");
            }
            if(boss == null)
                Destroy(gameObject);
            SendUpdateCheckToRoom();
        }
    }

    private void SendUpdateCheckToRoom() {
        foreach(Transform room in this.transform.root) {
            if(room.gameObject.activeInHierarchy) {
                room.gameObject.GetComponent<Room>().CheckForEnemies();
            }
        }
    }

    IEnumerator DamageAnimation() {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    public float GetEnemyHealth() {
        return health;
    }

}
