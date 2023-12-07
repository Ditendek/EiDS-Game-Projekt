using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    private List<GameObject> teleporter;

    public Vector2Int position;
    //private bool discovered = false;

    //private const int roomSizeX = 25; //Isaac: 13, 7 without outer walls
    //private const int roomSizeY = 25;

    // Start is called before the first frame update
    void Start() {
        teleporter = new List<GameObject>();
    }

    public void Link(GameObject otherRoom) {
        Room otherRoomRoomScript = otherRoom.GetComponent<Room>();
        Vector2Int direction = GetDirectionToOtherRoom(otherRoomRoomScript.position);

        string[] doors = { "upDoor", "rightDoor", "downDoor", "leftDoor" };
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

        for(int i = 0; i < 4; i++) {
            if(direction == directions[i]) {
                GameObject thisDoor = this.transform.Find(doors[i]).gameObject;
                GameObject otherDoor = otherRoom.transform.Find(doors[(i + 2) % 4]).gameObject;

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

    /*
     * Creates teleporter game objects and attaches them to the rooms.
     */
    /*public void LinkWithTTo<T>(Room otherRoom) where T : Teleporter
    {
        GameObject teleToOtherRoom = new("LinkToRoom" + otherRoom);     // Make teleporter game objects
        GameObject teleToThisRoom = new("LinkToRoom" + this);

        teleToOtherRoom.transform.SetParent(this.transform);            // Attach teleporter game objects to rooms
        teleToThisRoom.transform.SetParent(otherRoom.transform);

        T teleToOtherRoomComponent = teleToOtherRoom.AddComponent<T>(); // Attach teleporter scripts to teleporter game objects
        T teleToThisRoomComponent = teleToThisRoom.AddComponent<T>();

        teleToOtherRoomComponent.SetLinkedTeleporter(teleToThisRoom);   // Link the teleporters
        teleToThisRoomComponent.SetLinkedTeleporter(teleToOtherRoom);

        teleporter.Add(teleToOtherRoom);
        otherRoom.teleporter.Add(teleToThisRoom);
    }*/

    public override string ToString() {
        return "(" + position.x + "," + position.y + ")";
    }

    /*public void PlaceDoors()
    {
        foreach(GameObject doorGameObject in teleporter)
        {
            Door door = doorGameObject.GetComponent<Door>();

            if(door == null)
            {
                continue;
            }

            doorGameObject.transform.position = Vector2.zero + GetDoorPlacementDirection(door.doorFacingDirection) * (DoorIsUpOrDown(door.doorFacingDirection) ? roomSizeY / 2f : roomSizeX / 2f);
        }
    }*/

    /*private Vector2 GetDoorPlacementDirection(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Vector2.up,
            Direction.Down => Vector2.down,
            Direction.Left => Vector2.left,
            Direction.Right => Vector2.right,
            _ => Vector2.zero,
        };
    }*/

    /*private bool DoorIsUpOrDown(Direction direction)
    {
        return direction.Equals(Direction.Up) || direction.Equals(Direction.Down);
    }*/
}
