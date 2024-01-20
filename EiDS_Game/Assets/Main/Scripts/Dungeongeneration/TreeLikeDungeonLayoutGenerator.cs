using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeLikeDungeonLayoutGenerator : MonoBehaviour, IDungeonLayoutGenerator {
    [SerializeField]
    private int numberOfRooms;
    public float spawnPropability;

    private string[] _roomBuildArgs;
    private GameObject[] _rooms;
    private int currentNumberOfRooms;

    public void ResetToDefaultState() {
        _roomBuildArgs = new string[numberOfRooms];
        _rooms = new GameObject[numberOfRooms];
    }

    public void BuildDungeon() {
        int gridSize = (int) (2 * System.Math.Sqrt(_roomBuildArgs.Length));
        bool[,] roomAtPosition = new bool[gridSize, gridSize];

        int halfGridSize = gridSize / 2;
        Vector2Int startRoomPosition = new(halfGridSize, halfGridSize);
        
        GenerationLoop(roomAtPosition, startRoomPosition);

        if(currentNumberOfRooms <= 1) {
            Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
            Vector2Int roomPosition = startRoomPosition + directions[Random.Range(0, 4)];

            List<Vector2Int> neighboringRooms = GetNumberOfNeighboringRooms(roomPosition, roomAtPosition);
            AddNewRoomToListOfRooms(currentNumberOfRooms, roomPosition, neighboringRooms);
            roomAtPosition[roomPosition.x, roomPosition.y] = true;
            currentNumberOfRooms++;
        }

        AddEndKeyWordToLastRoom();
    }

    private void GenerationLoop(bool[,] roomAtPosition, Vector2Int startRoomPosition) {
        Queue<Vector2Int> roomQueue = new();
        roomQueue.Enqueue(startRoomPosition);
        
        for(currentNumberOfRooms = 0; currentNumberOfRooms < _roomBuildArgs.Length && roomQueue.Count != 0;) {
            Vector2Int roomPosition = roomQueue.Dequeue();
            List<Vector2Int> neighboringRooms = GetNumberOfNeighboringRooms(roomPosition, roomAtPosition);

            if(roomAtPosition[roomPosition.x, roomPosition.y] == true || neighboringRooms.Count > 1) {
                continue;
            }

            AddNewRoomToListOfRooms(currentNumberOfRooms, roomPosition, neighboringRooms);
            roomAtPosition[roomPosition.x, roomPosition.y] = true;
            currentNumberOfRooms++;

            GetNewPossibleRooms(roomQueue, roomPosition, roomAtPosition);
        }
    }

    private void AddEndKeyWordToLastRoom() {
        for(int i = _roomBuildArgs.Length - 1; i >= 0; i--) {
            if(_roomBuildArgs[i] != null) {
                _roomBuildArgs[i] += "end ";
                return;
            }
        }
    }

    private void GetNewPossibleRooms(Queue<Vector2Int> roomQueue, Vector2Int roomPosition, bool[,] roomAtPosition) {
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

        foreach(Vector2Int direction in directions) {
            Vector2Int newRoomPosition = roomPosition + direction;

            if(Random.Range(0f, 1f) <= spawnPropability &&
                PositionIsInBounds(newRoomPosition, roomAtPosition.GetLength(0)) &&
                roomAtPosition[newRoomPosition.x, newRoomPosition.y] == false) {
                roomQueue.Enqueue(newRoomPosition);
            }
        }
    }

    private void AddNewRoomToListOfRooms(int id, Vector2Int roomPosition, List<Vector2Int> neighboringRooms) {
        _roomBuildArgs[id] = roomPosition.x + " " + roomPosition.y + " ";
        foreach(Vector2Int neighbor in neighboringRooms) {
            _roomBuildArgs[id] += neighbor.x + " " + neighbor.y + " ";
        }
    }

    private List<Vector2Int> GetNumberOfNeighboringRooms(Vector2Int pos, bool[,] roomAtPosition) {
        List<Vector2Int> neighbors = new();
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
        foreach(Vector2Int direction in directions) {
            Vector2Int positionToCheck = pos + direction;

            if(PositionIsInBounds(positionToCheck, roomAtPosition.GetLength(0)) && roomAtPosition[positionToCheck.x, positionToCheck.y] == true) {
                neighbors.Add(positionToCheck);
            }
        }

        return neighbors;
    }

    private bool PositionIsInBounds(Vector2Int pos, int boundSize) {
        return pos.x >= 0 && pos.x < boundSize && pos.y >= 0 && pos.y < boundSize;
    }

    public void BuildRooms(IRoomLayoutGenerator roomLayoutGenerator) {
        for(int i = 0; i < _roomBuildArgs.Length; i++) {
            if(_roomBuildArgs[i] == null) {
                return;
            }

            _rooms[i] = roomLayoutGenerator.BuildRoom(_roomBuildArgs[i]);
        }
    }

    public void DeactivateAllRooms() {
        for(int i = 0; i < _rooms.Length; i++) {
            if(_rooms[i] == null) {
                return;
            }

            _rooms[i].SetActive(false);
        }
    }

    public GameObject GetStartRoom() {
        return _rooms[0];
    }

    public void SetNumberOfRooms(int n) {
        numberOfRooms = n;
    }

    public int GetNumberOfRooms() {
        return numberOfRooms;
    }
}
