using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLikeDungeonLayoutGenerator : MonoBehaviour, IDungeonLayoutGenerator
{
    public int numberOfRooms;
    public float spawnPropability;

    private string[] _roomBuildArgs;
    private GameObject[] _rooms;

    public void ResetToDefaultState() {
        _roomBuildArgs = new string[numberOfRooms];
        _rooms = new GameObject[numberOfRooms];
    }

    public void BuildDungeon()
    {
        int gridSize = (int) (2 * System.Math.Sqrt(_roomBuildArgs.Length));
        bool[,] roomAtPosition = new bool[gridSize, gridSize];

        int halfGridSize = gridSize / 2;
        Vector2Int startRoomPosition = new(halfGridSize, halfGridSize);

        GenerationLoop(roomAtPosition, startRoomPosition);
    }

    private void GenerationLoop(bool[,] roomAtPosition, Vector2Int startRoomPosition)
    {
        Queue<Vector2Int> roomQueue = new();
        roomQueue.Enqueue(startRoomPosition);

        for(int currentNumberOfRooms = 0; currentNumberOfRooms < _roomBuildArgs.Length && roomQueue.Count != 0;)
        {
            Vector2Int roomPosition = roomQueue.Dequeue();
            List<Vector2Int> neighboringRooms = GetNumberOfNeighboringRooms(roomPosition, roomAtPosition);

            if(roomAtPosition[roomPosition.x, roomPosition.y] == true || neighboringRooms.Count > 1)
            {
                continue;
            }

            AddRoomToListOfRooms(currentNumberOfRooms, roomPosition, neighboringRooms);
            roomAtPosition[roomPosition.x, roomPosition.y] = true;
            currentNumberOfRooms++;

            GetNewPossibleRooms(roomQueue, roomPosition, roomAtPosition);
        }

        AddEndKeyToLastRoom();
    }

    private void AddEndKeyToLastRoom()
    {
        for(int i = _roomBuildArgs.Length - 1; i >= 0; i--)
        {
            if(_roomBuildArgs.Equals("") == false)
            {
                _roomBuildArgs[i] += "end ";
                return;
            }
        }
    }

    private void GetNewPossibleRooms(Queue<Vector2Int> roomQueue, Vector2Int roomPosition, bool[,] roomAtPosition)
    {
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

        foreach(Vector2Int direction in directions)
        {
            Vector2Int newRoomPosition = roomPosition + direction;

            if(Random.Range(0f, 1f) <= spawnPropability &&
                PositionIsInBounds(newRoomPosition, roomAtPosition.GetLength(0)) &&
                roomAtPosition[newRoomPosition.x, newRoomPosition.y] == false)
            {
                roomQueue.Enqueue(newRoomPosition);
            }
        }
    }

    private void AddRoomToListOfRooms(int id, Vector2Int roomPosition, List<Vector2Int> neighboringRooms)
    {
        _roomBuildArgs[id] = roomPosition.x + " " + roomPosition.y + " ";
        foreach(Vector2Int neighbor in neighboringRooms)
        {
            _roomBuildArgs[id] += neighbor.x + " " + neighbor.y + " ";
        }
    }

    private List<Vector2Int> GetNumberOfNeighboringRooms(Vector2Int pos, bool[,] roomAtPosition)
    {
        List<Vector2Int> neighbors = new();
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
        foreach(Vector2Int direction in directions)
        {
            Vector2Int positionToCheck = pos + direction;

            if(PositionIsInBounds(positionToCheck, roomAtPosition.GetLength(0)) && roomAtPosition[positionToCheck.x, positionToCheck.y] == true)
            {
                neighbors.Add(positionToCheck);
            }
        }

        return neighbors;
    }

    private bool PositionIsInBounds(Vector2Int pos, int boundSize)
    {
        return pos.x >= 0 && pos.x < boundSize && pos.y >= 0 && pos.y < boundSize;
    }

    public void BuildRooms(IRoomLayoutGenerator roomLayoutGenerator)
    {
        for(int i = 0; i < _roomBuildArgs.Length; i++)
        {
            _rooms[i] = roomLayoutGenerator.BuildRoom(_roomBuildArgs[i]);
        }
    }

    public void DeactivateAllRooms()
    {
        for(int i = 0; i < _rooms.Length; i++)
        {
            _rooms[i].SetActive(false);
        }
    }

    public GameObject GetStartRoom()
    {
        return _rooms[0];
    }
}
