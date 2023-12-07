using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Teleporter
{
    public float doorSizeFacingSides = 3f;
    public float doorSizeSides = 1f;

    public Direction doorFacingDirection;

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
}

public enum Direction
{
    Up = 1,
    Down = -1,
    Right = 2,
    Left = -2,
}
