using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming,
        ChaseTarget,
    }
    private State state;
    public Transform enemyGFX;

    private PathFinding pathFinding;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private Vector3 lastPosition;
    private float checkInterval = 1.1f;
    private float timer;
    private AttackMode characterAim;
    public float attackRange = 7.5f;
    public float attackRate = 2f;
    public float chaseRange = 15f;

    private float attackCooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Roaming;
        pathFinding = GetComponent<PathFinding>();
        characterAim = GetComponent<AttackMode>();

        if (characterAim == null)
        {
            Debug.Log("ALARM, AttackMode Component not found");
            return;
        }


        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();

        pathFinding.SetTarget(roamPosition);

        lastPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        attackCooldown += Time.deltaTime;
        if (timer >= checkInterval)
        {
            CheckStuck();
            lastPosition = transform.position;
            timer = 0f;
        }
        CheckState();
        switch (state)
        {
            default:  
            case State.Roaming:
                pathFinding.ContinueMoving();
                float reachedPositionDistance = 2f;
                if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
                {
                    roamPosition = GetRoamingPosition();
                    pathFinding.SetTarget(roamPosition);
                }
                break;

            case State.ChaseTarget:
                pathFinding.SetTarget(PlayerMovement.PlayerPosition);
                 
                if (Vector3.Distance(transform.position, PlayerMovement.PlayerPosition) < attackRange){
                    
                    pathFinding.StopMoving();
                    Vector3 playerPosition = PlayerMovement.PlayerPosition;
                    if ((playerPosition.x < transform.position.x && enemyGFX.localScale.x > 0) ||
                        (playerPosition.x > transform.position.x && enemyGFX.localScale.x < 0))
                    {
                        enemyGFX.localScale = new Vector3(-enemyGFX.localScale.x, enemyGFX.localScale.y, enemyGFX.localScale.z);
                    }
            
                    characterAim.Aim();
                    if (attackCooldown >= attackRate)
                    {                      
                        characterAim.Attack();
                        attackCooldown = 0f;
                    }
                }
                else
                {
                    pathFinding.ContinueMoving();
                }
                break;
        }
    }


    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(10f, 5f);
    }
    private Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f)).normalized;
    }

    private void CheckState()
    {

        if (Vector3.Distance(transform.position, PlayerMovement.PlayerPosition) < chaseRange)
        {
            state = State.ChaseTarget;
        }
        else
        {
            state = State.Roaming;
            pathFinding.SetTarget(roamPosition);
        }

    }
    private void CheckStuck()
    {
        // In case the character is stuck for whatever reason
        if (Vector3.Distance(transform.position, lastPosition) < 1f)
        {
            roamPosition = GetRoamingPosition();
            pathFinding.SetTarget(roamPosition);
        }
    }

}
