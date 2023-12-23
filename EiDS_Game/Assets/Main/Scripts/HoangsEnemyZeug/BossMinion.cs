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

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spawner.spawnedEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
