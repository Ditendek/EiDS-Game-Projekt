using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    private Vector3 shootDir;
    public float moveSpeed = 3f;
    public void SetUp(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0,0,GetAngleFromVectorFloat(shootDir));
        //Despawn after ...f seconds
      //  Destroy(gameObject, 3f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


    private void Update()
    {
        transform.position += shootDir * Time.deltaTime * moveSpeed;
    }


    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
}
