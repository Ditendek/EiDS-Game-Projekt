using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    public List<GameObject> doors {
        private set; get;
    }

    public Vector2Int position;
    private bool isDiscovered = false;
    private bool isNextToDiscovered = false;

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

                thisDoor.AddComponent<Teleporter>().SetLinkedTeleporter(otherDoor);
                otherDoor.AddComponent<Teleporter>().SetLinkedTeleporter(thisDoor);

                float c = 80f / 256f;
                thisDoor.GetComponent<SpriteRenderer>().color = new Color(c, c, c);
                otherDoor.GetComponent<SpriteRenderer>().color = new Color(c, c, c);
            }
        }
    }

    private Vector2Int GetDirectionToOtherRoom(Vector2Int position) {
        return position - this.position;
    }

    public override string ToString() {
        return "(" + position.x + "," + position.y + ")";
    }
}
