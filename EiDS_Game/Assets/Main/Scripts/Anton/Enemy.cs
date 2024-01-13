using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour {
    public float health, maxHealth;
    public Dropables dropables;
    private BossMinion bossMinion;
    private BossAI boss;
    SpriteRenderer spriteRenderer;
    Color damageColor = new Color(1f, 0.5f, 0.5f, 1f);

    private void Start() {
        DungeonBuilder dungeon = GameObject.Find("Dungeon").GetComponent<DungeonBuilder>();
        float healthMultiplier = (dungeon.level - 1) * 0.5f + 1;
        maxHealth *= healthMultiplier;

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
            if(boss == null) {
                Destroy(gameObject);
            }
            if(dropables != null) {
                DropItem();
            }

            SendUpdateCheckToRoom();
        }
    }

    private void DropItem() {
        Dropables.Dropable dropable = dropables.getDropable();

        if(dropable.dropableGameobject == null) {
            return;
        }

        Transform room = getCurrentRoom();

        for(int i = 0; i < dropable.numberOfDrops; i++) {
            Vector2 randomOffset = Vector2.zero;

            if(dropable.numberOfDrops > 1) {
                randomOffset = UnityEngine.Random.insideUnitCircle.normalized * 3f;
            }

            GameObject drop = Instantiate(dropable.dropableGameobject, (Vector2) transform.position + randomOffset, Quaternion.identity);
            drop.transform.parent = room;
        }
    }

    private void SendUpdateCheckToRoom() {
        Transform room = getCurrentRoom();
        room.gameObject.GetComponent<Room>().CheckForEnemies();
    }

    private Transform getCurrentRoom() {
        foreach(Transform room in this.transform.root) {
            if(room.gameObject.activeInHierarchy) {
                return room;
            }
        }

        return null;
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
