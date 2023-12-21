using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDungeonLayoutGenerator
{
    public abstract void ResetToDefaultState();
    public abstract void BuildDungeon();
    public abstract GameObject GetStartRoom();
    public abstract void DeactivateAllRooms();
    public abstract void BuildRooms(IRoomLayoutGenerator roomLayoutGenerator);
    public abstract int GetNumberOfRooms();
    public abstract void SetNumberOfRooms(int n);
}
