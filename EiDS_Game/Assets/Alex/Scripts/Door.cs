using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Teleporter
{
    public float doorSizeFacingSides = 1f;
    public float doorSizeSides = 1f;

    private Direction doorFacingDirection;

    public void Start()
    {
        AddBoxCollider2DIfNotExistent();
        doorFacingDirection = Direction.Up;
        UpdateDoorSize();
    }

    public void SetDoorFacingDirection(Direction direction)
    {
        doorFacingDirection = direction;
        Door connectedDoor = GetLinkedTeleporter().GetComponent<Door>();
        connectedDoor.doorFacingDirection = (Direction) ((int) direction * -1);
        connectedDoor.UpdateDoorSize();
        UpdateDoorSize();
    }

    private void UpdateDoorSize()
    {
        if(Mathf.Abs((int) doorFacingDirection) == 1)
        {
            this.GetComponent<BoxCollider2D>().size = new Vector2(doorSizeFacingSides, doorSizeSides);
        }
        else
        {
            this.GetComponent<BoxCollider2D>().size = new Vector2(doorSizeSides, doorSizeFacingSides);
        }
    }

    /*private static int numberOfDoors = 0;
    
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
    }*/
}

public enum Direction
{
    Up = 1,
    Down = -1,
    Right = 2,
    Left = -2,
}
