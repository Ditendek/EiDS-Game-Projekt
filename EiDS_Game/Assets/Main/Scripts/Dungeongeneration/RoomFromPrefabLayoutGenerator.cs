using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Tilemaps;
using UnityEditor;
using System;



public class RoomFromPrefabLayoutGenerator : MonoBehaviour, IRoomLayoutGenerator {
    public string normalRoomPrefabsFolderPath = "Assets/Main/Rooms/normal";
    public string bossRoomPrefabsFolderPath = "Assets/Main/Rooms/boss";
    public GameObject minimapGameObject = null;
    public GameObject NextDungeonLoader = null;

    private GameObject[] normalRoomPrefabs;
    private GameObject[] bossRoomPrefabs;
    private List<GameObject> buildRooms;
    private string startRoom;

    public void ResetToDefaultState() {
        LoadPrefabsFromFolder();
        buildRooms = new List<GameObject>();
        startRoom = null;
    }

    private void LoadPrefabsFromFolder() {
        normalRoomPrefabs = LoadPrefabsFromFolder(normalRoomPrefabsFolderPath);
        bossRoomPrefabs = LoadPrefabsFromFolder(bossRoomPrefabsFolderPath);
    }

    private GameObject[] LoadPrefabsFromFolder(string folderPath) {
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { folderPath });
        GameObject[] storage = new GameObject[guids.Length];

        for(int i = 0; i < guids.Length; i++) {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            storage[i] = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        }

        return storage;
    }

    public GameObject BuildRoom(string args) {
        string[] formatedArgs = args.Split(' ');
        string roomName = "Room_" + formatedArgs[0] + "_" + formatedArgs[1];
        GameObject newRoom;


        if(IsEndRoom(formatedArgs)) {
            newRoom = Instantiate(bossRoomPrefabs[UnityEngine.Random.Range(0, bossRoomPrefabs.Length)], new Vector2(0, 0), Quaternion.identity, this.transform);
            GameObject nextDungeonLoader = Instantiate(NextDungeonLoader, newRoom.transform);
            newRoom.GetComponent<Room>().nextDungeonLoader = nextDungeonLoader;
            nextDungeonLoader.GetComponent<LoadNextDungeon>().DungeonBuilder = this.gameObject;
            nextDungeonLoader.SetActive(false);
        }
        else {
            newRoom = Instantiate(normalRoomPrefabs[UnityEngine.Random.Range(0, normalRoomPrefabs.Length)], new Vector2(0, 0), Quaternion.identity, this.transform);
        }

        newRoom.name = roomName;
        newRoom.GetComponent<Room>().ResetRoom();
        newRoom.GetComponent<Room>().position = new Vector2Int(int.Parse(formatedArgs[0]), int.Parse(formatedArgs[1]));
        newRoom.GetComponent<Room>().minimap = minimapGameObject;

        if(newRoomIsTheFirstRoom()) {
            startRoom = roomName;
            newRoom.GetComponent<Room>().IsDiscovered = true;
        }

        if(newRoomIsNextToStartRoom(formatedArgs)) {
            newRoom.GetComponent<Room>().IsNextToDiscovered = true;
        }

        ConnectRoomToRooms(newRoom, formatedArgs);

        AddRoomToBuildRooms(newRoom);

        return newRoom;
    }

    private bool IsEndRoom(string[] args) {
        for(int i = args.Length - 1; i >= 0; i--) {
            if(args[i].Equals("end")) {
                return true;
            }
        }

        return false;
    }

    private bool newRoomIsNextToStartRoom(string[] args) {
        for(int i = 2; i < args.Length; i++) {
            if((i + 1) < args.Length && int.TryParse(args[i], out int otherRoomX) && int.TryParse(args[i + 1], out int otherRoomY)) {
                i++;

                if(startRoom == GetRoomName(otherRoomX, otherRoomY)) {
                    return true;
                }
            }
        }

        return false;
    }

    private bool newRoomIsTheFirstRoom() {
        return buildRooms.Count == 0;
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
