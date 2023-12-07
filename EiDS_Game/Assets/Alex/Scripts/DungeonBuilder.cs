using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class DungeonBuilder : MonoBehaviour
{
    public int seed = 0;
    private Random.State randomState;

    private IDungeonLayoutGenerator _dungeonLayoutGenerator;
    private IRoomLayoutGenerator _roomLayoutGenerator;

    public void Start() {
        _dungeonLayoutGenerator = GetComponent<TreeLikeDungeonLayoutGenerator>();
        _roomLayoutGenerator = GetComponent<RoomFromPrefabLayoutGenerator>();

        Random.InitState(seed);
        randomState = Random.state;

        BuildDungeon();
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Return)) {
            BuildDungeon();
        }
    }

    public void BuildDungeon() {
        BuildDungeon(_dungeonLayoutGenerator, _roomLayoutGenerator);
    }

    public void BuildDungeon(IDungeonLayoutGenerator dungeonLayoutGenerator, IRoomLayoutGenerator roomLayoutGenerator)
    {
        ClearGameobject();
        dungeonLayoutGenerator.ResetToDefaultState();
        roomLayoutGenerator.ResetToDefaultState();

        Random.state = randomState;

        dungeonLayoutGenerator.BuildDungeon();
        dungeonLayoutGenerator.BuildRooms(roomLayoutGenerator);
        dungeonLayoutGenerator.DeactivateAllRooms();
        dungeonLayoutGenerator.GetStartRoom().SetActive(true);

        randomState = Random.state;
    }

    private void ClearGameobject() {
        foreach(Transform child in this.transform) {
            Destroy(child.gameObject);
        }
    }
}
