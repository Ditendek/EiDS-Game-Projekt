using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;



public class RoomFromPrefabLayoutGenerator : MonoBehaviour, IRoomLayoutGenerator {
    public RoomsList normalRooms;
    public RoomsList bossRooms;
    public GameObject firstRoomFirstLevel;
    public GameObject firstRoomOtherLevels;
    public GameObject minimapGameObject = null;
    public GameObject NextDungeonLoader = null;

    private List<GameObject> buildRooms;
    private string startRoom;
    private bool firstGeneration = true;

    public void ResetToDefaultState() {
        buildRooms = new List<GameObject>();
        startRoom = null;
    }

    public GameObject BuildRoom(string args) {
        string[] formatedArgs = args.Split(' ');
        string roomName = "Room_" + formatedArgs[0] + "_" + formatedArgs[1];
        GameObject newRoom;


        if(IsEndRoom(formatedArgs)) {
            newRoom = Instantiate(bossRooms.rooms[UnityEngine.Random.Range(0, bossRooms.rooms.Length)], new Vector2(0, 0), Quaternion.identity, this.transform);
            GameObject nextDungeonLoader = Instantiate(NextDungeonLoader, newRoom.transform);
            newRoom.GetComponent<Room>().nextDungeonLoader = nextDungeonLoader;
            nextDungeonLoader.GetComponent<LoadNextDungeon>().DungeonBuilder = this.gameObject;
            nextDungeonLoader.SetActive(false);
        }
        else if(firstGeneration) {
            firstGeneration = false;
            newRoom = Instantiate(firstRoomFirstLevel, new Vector2(0, 0), Quaternion.identity, this.transform);
        } else if(buildRooms.Count == 0) {
            newRoom = Instantiate(firstRoomOtherLevels, new Vector2(0, 0), Quaternion.identity, this.transform);
        } else {
            newRoom = Instantiate(normalRooms.rooms[UnityEngine.Random.Range(0, normalRooms.rooms.Length)], new Vector2(0, 0), Quaternion.identity, this.transform);
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
