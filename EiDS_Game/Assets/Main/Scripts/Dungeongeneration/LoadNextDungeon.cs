using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextDungeon : MonoBehaviour {
    public GameObject DungeonBuilder;

    protected void AddBoxCollider2DIfNotExistent() {
        BoxCollider2D bc = this.GetComponent<BoxCollider2D>();

        if(bc == null) {
            bc = this.gameObject.AddComponent<BoxCollider2D>();
        }

        bc.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            DungeonBuilder.GetComponent<DungeonBuilder>().IncreaseNumberOfRoomsBy(2);
            DungeonBuilder.GetComponent<DungeonBuilder>().BuildDungeon();
        }

        GameObject disposables = GameObject.Find("Disposables");
        for(int i = 0; i < disposables.transform.childCount; i++) {
            GameObject child = disposables.transform.GetChild(i).gameObject;
            Destroy(child);
        }
    }
}
