using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Room : MonoBehaviour {
    public List<GameObject> doors {
        private set; get;
    }

    public Vector2Int position;
    public GameObject minimap = null;
    public GameObject minimapEquivalent = null;
    public GameObject nextDungeonLoader = null;
    public Sprite currentRoomMinimapSprite;
    public Sprite enteredBeforeMinimapSprite;
    public Sprite notEnteredMinimapSprite;
    public EnemyGroups enemyGroups;
    private bool isDiscovered = false;
    private bool isNextToDiscovered = false;
    public bool isCleared = false;

    public bool IsDiscovered {
        get {
            return isDiscovered;
        }
        set {
            if(value) {
                isDiscovered = true;
                isNextToDiscovered = true;
            }
            else {
                isDiscovered = false;
            }
        }
    }

    public bool IsNextToDiscovered {
        get {
            return isNextToDiscovered;
        }
        set {
            if(value) {
                isNextToDiscovered = true;
            }
            else {
                isDiscovered = false;
                isNextToDiscovered = false;
            }
        }
    }


    //private const int roomSizeX = 25; //Isaac: 13, 7 without outer walls
    //private const int roomSizeY = 25;

    public void ResetRoom() {
        doors = new List<GameObject>();
        position = new Vector2Int(0, 0);
    }

    public void Link(GameObject otherRoom) {
        Room otherRoomRoomScript = otherRoom.GetComponent<Room>();
        Vector2Int direction = GetDirectionToOtherRoom(otherRoomRoomScript.position);

        string[] doors = { "upDoor", "rightDoor", "downDoor", "leftDoor" };
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

        for(int i = 0; i < 4; i++) {
            if(direction == directions[i]) {
                GameObject thisDoor = this.transform.Find(doors[i]).gameObject;
                this.doors.Add(thisDoor);
                GameObject otherDoor = otherRoom.transform.Find(doors[(i + 2) % 4]).gameObject;
                otherRoomRoomScript.doors.Add(otherDoor);

                thisDoor.GetComponent<Door>().enabled = true;
                otherDoor.GetComponent<Door>().enabled = true;

                thisDoor.GetComponent<Door>().SetLinkedDoor(otherDoor);
                otherDoor.GetComponent<Door>().SetLinkedDoor(thisDoor);

                thisDoor.GetComponent<Door>().Open();
                otherDoor.GetComponent<Door>().Open();
            }
        }
    }

    private Vector2Int GetDirectionToOtherRoom(Vector2Int position) {
        return position - this.position;
    }

    public void LoadEnemies() {
        CloseDoors();

        Instantiate(enemyGroups.enemieGroups[UnityEngine.Random.Range(0, enemyGroups.enemieGroups.Length)], new Vector2(0, 0), Quaternion.identity, this.transform);
    }

    public void CheckForEnemies() {
        foreach(Transform child in this.transform) {
            if(child.tag == "Enemy Group" && child.transform.childCount == 0) {
                isCleared = true;
                OpenDoors();
            }
        }
    }

    public void CloseDoors() {
        foreach(GameObject door in doors) {
            door.GetComponent<Door>().Close();
        }
    }

    public void OpenDoors() {
        foreach(GameObject door in doors) {
            door.GetComponent<Door>().Open();
        }
    }

    public void UpdateMinimap() {
        minimap.transform.position = (Vector3Int) (-position);

        if(minimapEquivalent == null) {
            minimapEquivalent = new GameObject(name);
            minimapEquivalent.transform.parent = minimap.transform;
            minimapEquivalent.transform.localPosition = (Vector3Int) position;
            minimapEquivalent.layer = LayerMask.NameToLayer("Minimap");
            minimapEquivalent.AddComponent<SpriteRenderer>();
        }
        minimapEquivalent.GetComponent<SpriteRenderer>().sprite = currentRoomMinimapSprite;

        foreach(GameObject doorToNextRoom in doors) {
            Room nextRoom = doorToNextRoom.GetComponent<Door>().GetLinkedDoor().transform.parent.gameObject.GetComponent<Room>();

            if(nextRoom.minimapEquivalent == null) {
                nextRoom.minimapEquivalent = new GameObject(nextRoom.name);
                nextRoom.minimapEquivalent.transform.parent = minimap.transform;
                nextRoom.minimapEquivalent.transform.localPosition = (Vector3Int) nextRoom.position;
                nextRoom.minimapEquivalent.layer = LayerMask.NameToLayer("Minimap");
                nextRoom.minimapEquivalent.AddComponent<SpriteRenderer>();
                nextRoom.minimapEquivalent.GetComponent<SpriteRenderer>().sprite = notEnteredMinimapSprite;
            }

            if(nextRoom.minimapEquivalent.GetComponent<SpriteRenderer>().sprite.name.Equals(notEnteredMinimapSprite.name) == false) {
                nextRoom.minimapEquivalent.GetComponent<SpriteRenderer>().sprite = enteredBeforeMinimapSprite;
            }
        }
    }

    public override string ToString() {
        return "(" + position.x + "," + position.y + ")";
    }
}
