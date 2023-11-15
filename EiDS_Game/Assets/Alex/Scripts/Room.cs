using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    List<Teleporter> teleporter;

    public Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        teleporter = new List<Teleporter>();
    }

    /*
     * Creates teleporter game objects and attaches them to the rooms.
     */
    public void LinkWithTTo<T>(Room otherRoom) where T : Teleporter
    {
        GameObject teleToOtherRoom = new("LinkToRoom" + otherRoom);     // Make teleporter game objects
        GameObject teleToThisRoom = new("LinkToRoom" + this);

        teleToOtherRoom.transform.SetParent(this.transform);            // Attach teleporter game objects to rooms
        teleToThisRoom.transform.SetParent(otherRoom.transform);

        T teleToOtherRoomComponent = teleToOtherRoom.AddComponent<T>(); // Attach teleporter scripts to teleporter game objects
        T teleToThisRoomComponent = teleToThisRoom.AddComponent<T>();

        teleToOtherRoomComponent.SetLinkedTeleporter(teleToThisRoom);   // Link the teleporters
        teleToThisRoomComponent.SetLinkedTeleporter(teleToOtherRoom);

        teleporter.Add(teleToOtherRoomComponent);
        otherRoom.teleporter.Add(teleToThisRoomComponent);
    }

    public override string ToString()
    {
        return "(" + (int) position.x + "," + (int) position.y + ")";
    }
}
