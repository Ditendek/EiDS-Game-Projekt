using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StabTree : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    public float timeToActivate = 3f;
    private CapsuleCollider2D coll;
     void Start()
    {
        coll = GetComponent<CapsuleCollider2D>();
        Destroy(gameObject, timeToActivate);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>= timeToActivate)
        {
            transform.localScale = new Vector3(0.7f, 1, 1);
            if (coll.bounds.Intersects(PlayerMovement.coll.bounds))
            {
                coll.isTrigger = false;
              //  Debug.Log("AUFGESPIEEEEESST");
            }

        }

    }
}
