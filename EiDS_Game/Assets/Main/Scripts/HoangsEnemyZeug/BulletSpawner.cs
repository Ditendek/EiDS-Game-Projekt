using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }


    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;


    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;
    [SerializeField] private float rotationSpeed = 2f;
    public Transform spawnPosition;



    private GameObject spawnedBullet;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //  if (spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + rotationSpeed*Time.deltaTime);
        if (spawnerType == SpawnerType.Spin)  RotateAroundCenter(spawnPosition, transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        if (timer >= firingRate)
        {
            Fire();
            timer = 0;
        }
    }
    void RotateAroundCenter(Transform target, Vector3 center, Vector3 axis, float angle)
    {
        // Um den Punkt center rotieren
        target.RotateAround(center, axis, angle);
    }
    private void Fire()
    {
        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, spawnPosition.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = spawnPosition.rotation;

        }
    }
}

