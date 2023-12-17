using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    [SerializeField]
    private GameObject linkedTeleporter;
    [SerializeField]
    private bool active = true;

    private void Start() {
        AddBoxCollider2DIfNotExistent();
    }

    /*
     * Adds a BoxCollider2D to the teleporter game object if their is none and makes it a trigger.
     */
    protected void AddBoxCollider2DIfNotExistent() {
        BoxCollider2D bc = this.GetComponent<BoxCollider2D>();

        if(bc == null) {
            bc = this.gameObject.AddComponent<BoxCollider2D>();
        }

        bc.isTrigger = true;
    }

    /*
     * Sets the teleporter.
     */
    public void SetLinkedTeleporter(GameObject linkedTeleporter) {
        this.linkedTeleporter = linkedTeleporter;
    }

    /*
     * Returns the teleporter
     */
    public GameObject GetLinkedTeleporter() {
        return linkedTeleporter;
    }

    /*
     * Puts the player to the room which is connected to the teleporter.
     */
    protected void OnTriggerEnter2D(Collider2D collision) {
        if(active && collision.gameObject.CompareTag("Player")) {
            linkedTeleporter.GetComponent<Teleporter>().active = false;
            collision.gameObject.transform.position = linkedTeleporter.transform.position;

            setDiscoveryStatus();

            LoadNextRoom(this.transform.parent.gameObject, linkedTeleporter.transform.parent.gameObject);
        }
    }

    private void setDiscoveryStatus() {
        linkedTeleporter.transform.parent.gameObject.GetComponent<Room>().IsDiscovered = true;
        foreach(GameObject door in linkedTeleporter.transform.parent.gameObject.GetComponent<Room>().doors) {
            door.transform.parent.gameObject.GetComponent<Room>().IsNextToDiscovered = true;
        }
    }

    public static void LoadNextRoom(GameObject RoomToUnload, GameObject RoomToLoad) {
        RoomToUnload.SetActive(false);
        RoomToLoad.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        active = true;
    }
}
