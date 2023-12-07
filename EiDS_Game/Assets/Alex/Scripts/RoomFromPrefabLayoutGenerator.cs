using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Tilemaps;
using UnityEditor;
using System;

public class RoomFromPrefabLayoutGenerator : MonoBehaviour, IRoomLayoutGenerator {
    public string folderPath = "Assets/Alex/Rooms";

    private GameObject[] roomPrefabs;
    private List<GameObject> buildRooms;

    public void ResetToDefaultState() {
        LoadPrefabsFromFolder();
        buildRooms = new List<GameObject>();
    }

    private void LoadPrefabsFromFolder() {
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { folderPath });
        roomPrefabs = new GameObject[guids.Length];

        for(int i = 0; i < guids.Length; i++) {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            roomPrefabs[i] = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        }
    }

    public GameObject BuildRoom(string arg) {
        string[] args = arg.Split(' ');
        string roomName = "Room_" + args[0] + "_" + args[1];

        GameObject newRoom = Instantiate(roomPrefabs[UnityEngine.Random.Range(0, roomPrefabs.Length)], new Vector2(0, 0), Quaternion.identity, this.transform);
        newRoom.name = roomName;
        newRoom.GetComponent<Room>().position = new Vector2Int(int.Parse(args[0]), int.Parse(args[1]));

        ConnectRoomToRooms(newRoom, args);

        AddRoomToBuildRooms(newRoom);

        return newRoom;
    }

    private void ConnectRoomToRooms(GameObject newRoom, string[] args) {
        Room newRoomRoomScript = newRoom.GetComponent<Room>();

        for(int i = 2; i < args.Length; i++) {
            if((i + 1) < args.Length && int.TryParse(args[i], out int otherRoomX) && int.TryParse(args[i + 1], out int otherRoomY)) {
                i++;
                newRoomRoomScript.Link(GetRoom(GetRoomName(otherRoomX, otherRoomY)));
            }
        }
    }

    // Can be Optimized with binary search.
    private GameObject GetRoom(string roomName) {
        foreach(GameObject room in buildRooms) {
            if(room.name == roomName) {
                return room;
            }
        }

        return null;
    }

    // Can be Optimized with binary search.
    private void AddRoomToBuildRooms(GameObject room) {
        buildRooms.Add(room);
    }

    private string GetRoomName(int x, int y) {
        return "Room_" + x + "_" + y;
    }
}
