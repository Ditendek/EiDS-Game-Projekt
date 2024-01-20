using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinion : MonoBehaviour
{
    private BossSpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectsWithTag("Boss")[0].GetComponent<BossSpawner>();
    }

    public void RemoveFromSpawner()
    {
        spawner.spawnedEnemies.Remove(gameObject);
    }
}
