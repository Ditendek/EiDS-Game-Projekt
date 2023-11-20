using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public void GenerateLevel(int seed)
    {
        Random.InitState(seed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GenerateLevel(Random.Range(int.MinValue, int.MaxValue));
        }
    }

    private GameObject GetNewRoom(Component[] components)
    {
        GameObject room = new();

        for(int i = 0; i < components.Length; ++i)
        {
            room.AddComponent(components[i].GetType());
        }

        return room;
    }
}
