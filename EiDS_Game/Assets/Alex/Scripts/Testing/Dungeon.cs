using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public GameObject player;
    
    private int dungeonLevel = 0;

    public void GenerateDungeon(int seed, int level)
    {
        UnityEngine.Random.InitState(seed);
        dungeonLevel = level;

        //DungeonLayoutGenerator.TreeLikeGeneration(7 + 3 * dungeonLevel);
        //GameObject spawnRoom = DungeonLayoutGenerator.GetSpawnRoom();

        //spawnRoom.SetActive(true);
        //player.transform.position = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GenerateDungeon(UnityEngine.Random.Range(int.MinValue, int.MaxValue), dungeonLevel + 1);
            collision.gameObject.transform.position = Vector2.zero;
        }
    }
}
