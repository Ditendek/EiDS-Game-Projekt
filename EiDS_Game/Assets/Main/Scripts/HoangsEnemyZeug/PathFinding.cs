using UnityEngine;
using Pathfinding;

public class PathFinding : MonoBehaviour
{
    

    public Vector3 target;
    public Transform enemyGFX;
    public float speed = 200f;
    Rigidbody2D rb;
    private bool moving = true;
    //how close the enemy needs to be to a waypoint to move to the next one
    public float nextWaypointDistance = 1f;
    Path path;
    int currentWaypoint = 0;
   

    //reponsible for creating paths
    private Seeker seeker;
    

    // Start is called before the first frame update
    void Start()
    {
    //    AstarPath.active.Scan(); //can be used for every room
 
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = transform.position;
        //update the path every half second
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    public void SetTarget(Vector3 targetPosition)
    {
        if (seeker.IsDone())
            target = targetPosition;
     }

 

    void UpdatePath()
    {
        if (target == transform.position) return;
        // seeker currently not calculating a path   

       // var gg = AstarPath.active.data.gridGraph;
        if (seeker.IsDone())
        // generate a path 
        seeker.StartPath(rb.position, target, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        //set new path if there is no error
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    // Update is called a fix number of times per second
    // ideal when working with physics 
    void FixedUpdate()
    {
       // Debug.Log(path==null);

        if (path == null || !moving)
        {
            return;
        }
        // if currentWaypoint is greater than the total amount of WayPoints along the path
        //maybe necessary later
        /*
         if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }*/

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * Time.deltaTime * speed;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // the desired velocity the AI wants to travel with based on the current path
        if (force.x > 0.03f && enemyGFX.localScale.x<0)
        {
            enemyGFX.localScale = new Vector3(-enemyGFX.localScale.x, enemyGFX.localScale.y, enemyGFX.localScale.z);
        }
        else if (force.x <= -0.03f && enemyGFX.localScale.x>0)
        {
            enemyGFX.localScale = new Vector3(-enemyGFX.localScale.x, enemyGFX.localScale.y, enemyGFX.localScale.z);
        }
    }
    public void StopMoving()
    {
        moving = false;
    }
    public void ContinueMoving()
    {
        moving = true;
    }
}
