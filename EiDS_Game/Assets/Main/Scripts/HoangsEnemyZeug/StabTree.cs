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
    private bool once = true;
     void Start()
    {
        coll = GetComponent<CapsuleCollider2D>();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (once)
        {
            timer += Time.deltaTime;
            if (timer >= timeToActivate)
            {
                Grow();
                once = false;
            }
        }
    }
    void Grow()
    {
        transform.localScale = new Vector3(0.7f, 1f, 1);
        transform.position = transform.position + new Vector3(0, 0.6f, 0);
    }
}
