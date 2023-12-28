using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField]
    private GameObject linkedDoor;
    [SerializeField]
    private bool active = true;
    [SerializeField]
    private Sprite openDoorSprite;
    [SerializeField]
    private Sprite closedDoorSprite;

    private void Start() {
        AddBoxCollider2DIfNotExistent();
    }

    protected void AddBoxCollider2DIfNotExistent() {
        BoxCollider2D bc = this.GetComponent<BoxCollider2D>();

        if(bc == null) {
            bc = this.gameObject.AddComponent<BoxCollider2D>();
        }

        bc.isTrigger = true;
    }

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
    }

    public void Open() {
        GetComponent<SpriteRenderer>().sprite = openDoorSprite;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void Close() {
        GetComponent<SpriteRenderer>().sprite = closedDoorSprite;
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
