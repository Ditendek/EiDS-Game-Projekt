using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField]
    private GameObject linkedDoor = null;
    [SerializeField]
    private bool active = true;
    [SerializeField]
    private Sprite openDoorSprite;
    [SerializeField]
    private Sprite closedDoorSprite;

    public void SetLinkedDoor(GameObject linkedDoor) {
        this.linkedDoor = linkedDoor;
    }

    public GameObject GetLinkedDoor() {
        return linkedDoor;
    }

    /*
     * Puts the player to the room which is connected to the teleporter.
     */
    private void OnTriggerEnter2D(Collider2D collision) {
        if(active && collision.gameObject.CompareTag("Player")) {
            foreach(Transform child in linkedDoor.transform) {
                if(child.name == "SpawnPoint") {
                    collision.gameObject.transform.position = child.transform.position;
                }
            }

            SetDiscoveryStatus();

            GameObject disposables = GameObject.Find("Disposables");
            for(int i = 0; i < disposables.transform.childCount; i++) {
                GameObject child = disposables.transform.GetChild(i).gameObject;
                Destroy(child);
            }

            LoadNextRoom(this.transform.parent.gameObject, linkedDoor.transform.parent.gameObject);
        }
    }

    private void SetDiscoveryStatus() {
        linkedDoor.transform.parent.gameObject.GetComponent<Room>().IsDiscovered = true;
        foreach(GameObject door in linkedDoor.transform.parent.gameObject.GetComponent<Room>().doors) {
            door.transform.parent.gameObject.GetComponent<Room>().IsNextToDiscovered = true;
        }
    }

    public static void LoadNextRoom(GameObject RoomToUnload, GameObject RoomToLoad) {
        RoomToUnload.SetActive(false);
        RoomToLoad.SetActive(true);
        RoomToLoad.GetComponent<Room>().UpdateMinimap();

        if(RoomToLoad.GetComponent<Room>().isCleared == false) {
            RoomToLoad.GetComponent<Room>().LoadEnemies();
        }

        AstarPath.active.Scan();
    }

    public void Open() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void Close() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = closedDoorSprite;
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
