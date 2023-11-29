using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public int numberOfRooms = 1;
    public int gridSize = 1;
    public int seed = 0;
    public GameObject roomPrefab = null;

    private Room[,] roomAtGridPosition = null;

    // Start is called before the first frame update
    void Start()
    {
        /*foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }*/
        
        roomAtGridPosition = new Room[gridSize, gridSize];
        /*for(int i = 0; i < gridSize * gridSize; ++i)
        {
            roomAtGridPosition[i / gridSize, i % gridSize].roomGameObject = null;
        }*/

        SpawnTreeFromCenter(seed);
    }

    /* 
     * 
     */
    void SpawnTreeFromCenter(int seed)
    {
        Random.InitState(seed);

        int halfGrid = gridSize / 2;
        Pair pos = new(halfGrid, halfGrid);
        Room startRoom = new(pos, null);
        //roomAtGridPosition[halfGrid, halfGrid] = startRoom;

        TreeLikeGeneration(startRoom);
    }

    /* 
     * 
     */
    void TreeLikeGeneration(Room startingRoom)
    {
        Queue<Room> roomQueue = new();
        roomQueue.Enqueue(startingRoom);
        Pair[] directions = { new(0, -1), new(1, 0), new(0, 1), new(-1, 0)};

        int currentNumberOfRooms = 0;
        while(currentNumberOfRooms < numberOfRooms && roomQueue.Count != 0)
        {
            Room room = roomQueue.Dequeue();

            if(roomAtGridPosition[room.pos.x, room.pos.y].roomGameObject != null)
            {
                continue;
            }

            List<Direction> neighbors = GetDirectNeighbors(room.pos);

            if(neighbors.Count > 1)
            {
                continue;
            }

            room.roomGameObject = InstantiateRoom(room.pos);
            roomAtGridPosition[room.pos.x, room.pos.y] = room;
            currentNumberOfRooms++;

            for(int i = 0; i < 4; ++i)
            {
                Pair newPos = room.pos + directions[i];

                if(Random.Range(0f, 1f) <= 0.70f && PositionIsInBounds(newPos) && roomAtGridPosition[newPos.x, newPos.y].roomGameObject == null)
                {
                    Room newRoom = new(newPos, null);
                    roomQueue.Enqueue(newRoom);
                }
            }
        }
    }

    /* 
     * 
     */
    List<Direction> GetDirectNeighbors(Pair pos)
    {
        List<Direction> neighbors = new();

        if(!PositionIsInBounds(pos))
        {
            throw new System.ArgumentOutOfRangeException("Position coordinates (" + pos.x + ", " + pos.y + ") are out of bounds (0 - " + (gridSize - 1) + ")");
        }

        if(pos.y > 0 && roomAtGridPosition[pos.x, pos.y - 1].roomGameObject != null)
        {
            neighbors.Add(Direction.Up);
        }
        if(pos.x < gridSize - 1 && roomAtGridPosition[pos.x + 1, pos.y].roomGameObject != null)
        {
            neighbors.Add(Direction.Right);
        }
        if(pos.y < gridSize - 1 && roomAtGridPosition[pos.x, pos.y + 1].roomGameObject != null)
        {
            neighbors.Add(Direction.Down);
        }
        if(pos.x > 0 && roomAtGridPosition[pos.x - 1, pos.y].roomGameObject != null)
        {
            neighbors.Add(Direction.Left);
        }

        return neighbors;
    }

    /* 
     * 
     */
    bool PositionIsInBounds(Pair pos)
    {
        return pos.x >= 0 && pos.x < gridSize && pos.y >= 0 && pos.y < gridSize;
    }

    /* 
     * 
     */
    GameObject InstantiateRoom(Pair pos)
    {
        GameObject room = Instantiate(roomPrefab, this.transform);
        room.name = "Room_" + pos.x + "_" + pos.y;
        room.transform.position = new Vector2(pos.x, pos.y);

        return room;
    }

    /* 
     * 
     */
    private struct Room
    {
        public Pair pos;
        public GameObject roomGameObject;

        public Room(Pair pos, GameObject roomGameobject)
        {
            this.pos = pos;
            this.roomGameObject = roomGameobject;
        }
    }

    /* 
     * 
     */
    private struct Pair
    {
        public int x, y;

        public Pair(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Pair operator +(Pair a, Pair b)
        {
            return new(a.x + b.x, a.y + b.y);
        }
    }

    /* 
     * 
     */
    enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }
}
