using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public int numberOfRooms = 1;
    public int gridSize = 1;
    public GameObject roomProfab = null;

    private int currentNumberOfRooms = 0;
    private bool[,] roomSpawnedAtGridPosition = null;

    // Start is called before the first frame update
    void Start()
    {
        roomSpawnedAtGridPosition = new bool[gridSize, gridSize];

        spawnFromCenter();
    }

    void spawnAtRandomLocations()
    {
        for(int i = 0; i < numberOfRooms; i++)
        {
            int xPos = Random.Range(0, gridSize);
            int yPos = Random.Range(0, gridSize);

            if(roomSpawnedAtGridPosition[xPos, yPos] == false)
            {
                ++currentNumberOfRooms;
                instantiateRoom(new Vector2(xPos, yPos));

                //GameObject room = new GameObject("Room_" + xPos + "_" + yPos);
                //room.transform.parent = this.transform;
            }
        }
    }

    void spawnFromCenter()
    {
        int halfGrid = gridSize / 2;
        ArrayList

        instantiateRoom(new Vector2(halfGrid, halfGrid));
    }

    GameObject instantiateRoom(Vector2 position)
    {
        roomSpawnedAtGridPosition[(int) position.x, (int) position.y] = true;

        GameObject room = Instantiate(roomProfab, this.transform);
        room.name = "Room_" + (int) position.x + "_" + (int) position.y;
        room.transform.position = position;

        return room;
    }

    struct EndRoom
    {
        public EndRoom()
        {
        
        }
    }

    enum Walltype
    {
        Wall,
        Door
    }
}
