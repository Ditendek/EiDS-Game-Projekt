using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossSpawner : MonoBehaviour
{
    private Animator animator;
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    private Rigidbody2D rb; 
    [SerializeField] private GameObject slimeBossPrefab;
    [SerializeField] private float spawnInterval = 0.5f;//randomization possible
    [SerializeField] private int maxSpawnAmount = 5;
    [SerializeField] private float spawnRangeX = 10f;
    [SerializeField] private float spawnRangeY = 10f;
 
    public bool enableSpawnerOnce = true;
    public bool start1stRoutine = false;
    public bool doneSpawning = false;   
    private int spawnCount = 0;
    private BossAI bossAI;
    private Vector3 startPosition;

    // COULD ADD CASE where enemies spawn into existion objects later
    public void Start()
    {
        bossAI = GetComponent<BossAI>();
        rb = GetComponent<Rigidbody2D>();
        animator = transform.Find("BOSS_GFX").GetComponent<Animator>();
    }

    public void Update()    {

        if (bossAI.GetState() == BossAI.State.Spawning &&
            enableSpawnerOnce &&
            Vector3.Distance(transform.position, startPosition)<1f)
        {
            rb.velocity = new Vector2(0, 0);
            EnableSpawner();
            return;
        }


        if (start1stRoutine && Vector3.Distance(transform.position, startPosition) < 1f)
        {
            start1stRoutine = false;
            animator.SetTrigger("spawn-phase");

            StartCoroutine(spawnEnemy(spawnInterval, slimeBossPrefab));
            return;
        }

    }
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        animator.SetTrigger("spawning");
        GameObject newEnemy = Instantiate(enemy,
                            new Vector3(Random.Range(transform.position.x - spawnRangeX / 2, transform.position.x +  spawnRangeX / 2),
                                        Random.Range(transform.position.y - spawnRangeY / 2, transform.position.y + spawnRangeY / 2),
                                        0), Quaternion.identity);
        newEnemy.transform.parent = transform.parent.parent;
        spawnedEnemies.Add(newEnemy);
        spawnCount++;
        if (spawnCount < maxSpawnAmount)
        {
            StartCoroutine(spawnEnemy(interval, enemy));
        }
        else
        {
            doneSpawning = true;
        }
 
    }
    public void EnableSpawner()
    {
        enableSpawnerOnce = false;
        start1stRoutine = true;
    }

    public void DisableSpawner()
    {
        enableSpawnerOnce = true;
        spawnCount = 0;
    }
    public void SetStartPosition(Vector3 position)
    {
        this.startPosition = position;
    }
}
