using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    public float maxHealth;
    public float health;
  //  public int damage;
    public float damagePhaseTimer;
    // public Slider healthBar;
    //public float maxSpeed;
 
    private Animator animator;

    private float timeBtwDamage =0.2f;
    private Vector3 spawnPosition;
    private Rigidbody2D rb;
    Bounce bouncer;
    BossSpawner spawnHelper;
    NotMyTrailRenderer trailRenderer; 

    private bool isScaling = false;
    private bool doneScaling = false;
    private float scalingTimeCounter = 0;
    public enum State
    {
        Scaling,
        Bouncing,
        Spawning,
        Damage,
    }
    public State state;
    public bool triggerOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = transform.Find("BOSS_GFX").GetComponent<Animator>();
        trailRenderer = transform.Find("BOSS_GFX").GetComponent<NotMyTrailRenderer>();

        bouncer = transform.GetComponent<Bounce>();

     
        spawnHelper = transform.GetComponent<BossSpawner>();
        spawnHelper.enabled = false;
        spawnHelper.SetStartPosition(spawnPosition);
        StartCoroutine(scaleOverTime(transform.Find("BOSS_GFX"), new Vector3(2, 2, 1), 2f));
    }

    // Update is called once per frame
    void Update()
    {
 
        CheckState();
    //    if(GetState() == State.Bouncing) Debug.Log(rb.velocity.magnitude);

        switch (state)
        {
            default:
            case State.Scaling:
                trailRenderer.enabled = true;
                StartCoroutine(scaleOverTime(transform.Find("BOSS_GFX"), new Vector3(3, 3, 3), 10f));
                break;
             case State.Bouncing:             
                bouncer.SetBounceMode(true);
                trailRenderer.ClonesPerSecond  = 30;
                spawnHelper.enabled = false;
                break;
            case State.Spawning:
                bouncer.SetBounceMode(false);
                trailRenderer.ClonesPerSecond = 0;
                rb.velocity = new Vector2(0.1f,0.1f);
                if (triggerOnce)
                {
                    animator.SetTrigger("run");
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    triggerOnce = false;
                }
                spawnHelper.enabled = true;
                 
                float positionThreshold = 0.1f;
                if (Vector3.Distance(transform.position,spawnPosition) > positionThreshold  && transform.position.x > spawnPosition.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);

                }
                transform.position = Vector3.MoveTowards(transform.position, spawnPosition, 0.025f);
                break;
            case State.Damage:
            {
               rb.velocity = new Vector2(0,0);
               break;
            }
        }
        // give the player some time to recover before taking more damage
        if (timeBtwDamage > 0)
        {
            timeBtwDamage -= Time.deltaTime;    
        }

    //    healthBar.value = health/maxHealth;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TakeDamage(1000);
        }
    }
    
    public void  TakeDamage(int damage)
    {
        if (GetState() == State.Spawning) return;
        //Take damage itself for demo reasons
        if (timeBtwDamage <= 0)
        {
            health -= damage;
            timeBtwDamage = 0.2f;
        }
    }
 

    private void CheckState()
    {
        //Scale  Once At The Start
        if (!doneScaling)
        {
            state = State.Scaling;
            return;
        }
        //Switch from bouncing to spawning
        if(!spawnHelper.doneSpawning && bouncer.GetBounceAmount() >=  bouncer.GetMaxBounceAmount())
        {
            state = State.Spawning;
            return;
        }

        //switch from spawning to damage phase
        if (state == State.Spawning && spawnHelper.doneSpawning && spawnHelper.spawnedEnemies.Count <= 0)
        {
            state = State.Damage;
            StartCoroutine(Wait(damagePhaseTimer));
            return;

        }
       
        
    }
    IEnumerator Wait(float seconds)
    {
         yield return new WaitForSeconds(seconds);
         //switch from damage phase to bouncing again
        spawnHelper.DisableSpawner();
        spawnHelper.doneSpawning = false;
        bouncer.StartBouncing(); //sets bounceamount to 0
        state = State.Bouncing;
        triggerOnce = true;
    }
    IEnumerator scaleOverTime(Transform objectToScale, Vector3 toScale, float duration)
    {
         if (isScaling)
        {
            yield break;
        }
        isScaling = true;


        Vector3 startScaleSize = objectToScale.localScale;
        while (scalingTimeCounter < duration)
        {
             scalingTimeCounter += Time.deltaTime;
            objectToScale.localScale = Vector3.Lerp(startScaleSize, toScale, scalingTimeCounter / duration);
            if(scalingTimeCounter/duration >= 1)
            {
                doneScaling = true;
                state = State.Bouncing;
                bouncer.StartBouncing();
             }
            yield return null;
        }
        isScaling = false;
    }
    public float GetHealth()
    {
        return health;
    }


 
    public State GetState()
    {
        return state;
    }



}
