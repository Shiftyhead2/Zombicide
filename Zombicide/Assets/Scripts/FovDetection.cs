using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovDetection : MonoBehaviour
{
    [SerializeField] float viewRadius = 5;
    [SerializeField] float viewAngle = 135;
    [SerializeField] LayerMask obstacleMask, playerMask;
    Collider2D[] playerinRadius;
    public List<Transform> VisiblePlayer = new List<Transform>();
    EnemyAI Ai;


    private void Start()
    {
        Ai = GetComponent<EnemyAI>();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

    void FixedUpdate()
    {
        FindVisiblePlayer();
    }

    void FindVisiblePlayer()
    {
        playerinRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius);

        VisiblePlayer.Clear();

        for(int i= 0; i < playerinRadius.Length; i++)
        {
            Transform player = playerinRadius[i].transform;
            Vector2 dirTarget = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
            if (Vector2.Angle(dirTarget, transform.up) < viewAngle / 2)
            {
                float distancePlayer = Vector2.Distance(transform.position, player.position);

                if (!Physics2D.Raycast(transform.position, dirTarget, distancePlayer, obstacleMask)) {
                    if (Physics2D.Raycast(transform.position, dirTarget, distancePlayer, playerMask))
                    {
                        VisiblePlayer.Add(player);
                        if (!Ai.DetectedTarget)
                        {
                            Ai.DetectedTarget = true;
                        }
                        

                    }
                    else
                    {
                        //Do nothing
                    }
                    
                }

            }
        }
    }

    public Vector2 DirFromAngle(float angleDeg,bool global)
    {
        if (!global)
        {
            angleDeg += transform.eulerAngles.z;
        }

        return new Vector2(Mathf.Sin(angleDeg * Mathf.Deg2Rad), Mathf.Cos(angleDeg * Mathf.Deg2Rad));
    }

}
