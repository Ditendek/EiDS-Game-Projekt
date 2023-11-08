using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private static int numberOfDoors = 0;
    
    private readonly int id;
    private readonly DoorType type;
    private readonly Facing facingDirection;
    private readonly Door connectedDoor;

    public Door(DoorType type, Facing facingDirection)
    {
        id = numberOfDoors;
        ++numberOfDoors;
        this.type = type;
        this.facingDirection = facingDirection;

        connectedDoor = new Door(this);
    }

    private Door(Door connectedDoor)
    {
        id = connectedDoor.id;
        type = connectedDoor.type;
        facingDirection = (Facing) ((int) connectedDoor.facingDirection * -1);
        this.connectedDoor = connectedDoor;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public enum DoorType
    {
        Normal
    }

    public enum Facing
    {
        Up = 1,
        Down = -1,
        Left = 2,
        Right = -2
    }
}
