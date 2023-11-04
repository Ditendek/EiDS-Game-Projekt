using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public int maxNumberOfRooms = 1;
    public int gridSize = 1;
    public GameObject roomProfab = null;

    private int currentNumberOfRooms = 0;
    private bool[,] roomSpawnedAtGridPosition = null;
    
    // Start is called before the first frame update
    void Start()
    {
        roomSpawnedAtGridPosition = new bool[gridSize, gridSize];

        spawnAtRandomLocations();
    }

    void spawnAtRandomLocations()
    {
        for(int i = 0; i < maxNumberOfRooms; i++)
        {
            int xPos = Random.Range(0, gridSize);
            int yPos = Random.Range(0, gridSize);

            if(roomSpawnedAtGridPosition[xPos, yPos] == false)
            {
                ++currentNumberOfRooms;
                roomSpawnedAtGridPosition[xPos, yPos] = true;

                GameObject room = Instantiate(roomProfab, this.transform);
                room.name = "Room_" + xPos + "_" + yPos;
                room.transform.position = new Vector2(xPos, yPos);
                //GameObject room = new GameObject("Room_" + xPos + "_" + yPos);
                //room.transform.parent = this.transform;
            }
        }
    }
}
