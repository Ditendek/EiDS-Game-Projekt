using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    [SerializeField]
    private GameObject linkedTeleporter;
    [SerializeField]
    private bool active = true;
    [SerializeField]
    private Sprite open;

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

    public void SetLinkedTeleporter(GameObject linkedTeleporter) {
        this.linkedTeleporter = linkedTeleporter;
    }

    public GameObject GetLinkedTeleporter() {
        return linkedTeleporter;
    }

    /*
     * Puts the player to the room which is connected to the teleporter.
     */
    private void OnTriggerEnter2D(Collider2D collision) {
        if(active && collision.gameObject.CompareTag("Player")) {
            //linkedTeleporter.GetComponent<Teleporter>().active = false;
            foreach(Transform child in linkedTeleporter.transform) {
                if(child.name == "SpawnPoint") {
                    collision.gameObject.transform.position = child.transform.position;
                }
            }

            SetDiscoveryStatus();
            //UpdateMinimap(linkedTeleporter.transform.parent.gameObject.GetComponent<Room>().position);

            LoadNextRoom(this.transform.parent.gameObject, linkedTeleporter.transform.parent.gameObject);
        }
    }

    private void SetDiscoveryStatus() {
        linkedTeleporter.transform.parent.gameObject.GetComponent<Room>().IsDiscovered = true;
        foreach(GameObject door in linkedTeleporter.transform.parent.gameObject.GetComponent<Room>().doors) {
            door.transform.parent.gameObject.GetComponent<Room>().IsNextToDiscovered = true;
        }
    }

    public static void LoadNextRoom(GameObject RoomToUnload, GameObject RoomToLoad) {
        RoomToUnload.SetActive(false);
        RoomToLoad.SetActive(true);
        RoomToLoad.GetComponent<Room>().UpdateMinimap();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //active = true;
    }
}
