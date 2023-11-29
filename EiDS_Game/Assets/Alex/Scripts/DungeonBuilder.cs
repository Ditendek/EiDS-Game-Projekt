using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBuilder : MonoBehaviour
{
    public void BuildDungeon(IDungeonLayoutGenerator dungeonLayoutGenerator, IRoomLayoutGenerator roomLayoutGenerator)
    {
        dungeonLayoutGenerator.BuildDungeon();
        dungeonLayoutGenerator.BuildRooms(roomLayoutGenerator);
        dungeonLayoutGenerator.DeactivateAllRooms();
        dungeonLayoutGenerator.GetStartRoom().SetActive(true);
    }

}
