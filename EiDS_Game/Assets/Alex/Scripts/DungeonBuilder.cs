using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class DungeonBuilder : MonoBehaviour {
    public int seed = 0;
    public GameObject minimap;
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

    public void BuildDungeon(IDungeonLayoutGenerator dungeonLayoutGenerator, IRoomLayoutGenerator roomLayoutGenerator) {
        ClearGameobject();
        dungeonLayoutGenerator.ResetToDefaultState();
        roomLayoutGenerator.ResetToDefaultState();

        Random.state = randomState;

        dungeonLayoutGenerator.BuildDungeon();
        dungeonLayoutGenerator.BuildRooms(roomLayoutGenerator);
        dungeonLayoutGenerator.DeactivateAllRooms();
        dungeonLayoutGenerator.GetStartRoom().SetActive(true);
        dungeonLayoutGenerator.GetStartRoom().GetComponent<Room>().UpdateMinimap();

        randomState = Random.state;
    }

    /*
     * Required for generating new dungeon
     */
    private void ClearGameobject() {
        foreach(Transform child in this.transform) {
            Destroy(child.gameObject);
        }

        foreach(Transform child in minimap.transform) {
            Destroy(child.gameObject);
        }
    }

    public void IncreaseNumberOfRoomsBy(int n) {
        _dungeonLayoutGenerator.SetNumberOfRooms(_dungeonLayoutGenerator.GetNumberOfRooms() + n);
    }
}
