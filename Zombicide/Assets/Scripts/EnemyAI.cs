using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform Target;
    public bool DetectedTarget = false;
    public float MaxSpeed;
    float MoveSpeed;
    public float nextWaypointDistance = 3f;
    [SerializeField] float PathCooldown = 1f;
    Path path;
    int currentWaypoint;
    bool reachedEndofPath = false;
    Vector2 Movement;

    Seeker seeker;
    Rigidbody2D RB;



    void Start()
    {
        seeker = GetComponent<Seeker>();
        RB = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, PathCooldown);
        


    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(RB.position, Target.position, OnPathComplete);
        }
    }

    private void Update()
    {
        if (DetectedTarget)
        {
            Vector3 lookDir = Target.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            RB.rotation = angle;
        }
    }

    private void FixedUpdate()
    {
        if (DetectedTarget)
        {
            MoveSpeed = MaxSpeed;
            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndofPath = true;
                return;
            }
            else
            {
                reachedEndofPath = false;
                
            }

            Movement = ((Vector2)path.vectorPath[currentWaypoint] - RB.position).normalized;
            Vector2 force = Movement * MoveSpeed * Time.fixedDeltaTime;
            Vector2 velocity = RB.velocity;
            velocity = force;
            RB.velocity = velocity;
           
            

            float distance = Vector2.Distance(RB.position, path.vectorPath[currentWaypoint]);
            

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
        else
        {
            MoveSpeed = 0;
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

   
}
