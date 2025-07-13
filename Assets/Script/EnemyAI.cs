using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 2f;
    public float detectionRadius = 3f;
    public float chaseRadius = 5f;
    public LayerMask playerLayer;

    private int currentPoint = 0;
    private enum State { Patrol, Chase, ReturnToPatrol }
    private State state = State.Patrol;

    private Transform player;
    private PlayerMovement playerCtrl;
    private Vector3 lastKnownPos;

    void Start()
    {
        if (patrolPoints.Length == 0)
            enabled = false;

        player = GameObject.FindWithTag("Player").transform;
        playerCtrl = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        switch (state)
        {
            case State.Patrol:
                Patrol();
                DetectPlayer();
                break;

            case State.Chase:
                Chase();
                break;

            case State.ReturnToPatrol:
                ReturnToPatrol();
                break;
        }
    }

    void Patrol()
    {
        Transform target = patrolPoints[currentPoint];
        MoveTowards(target.position);

        if (Vector2.Distance(transform.position, target.position) < 0.2f)
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }

    void DetectPlayer()
    {
        if (playerCtrl.isHiding)
            return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= detectionRadius)
        {
            lastKnownPos = player.position;
            state = State.Chase;
        }
    }

    void Chase()
    {
        if (playerCtrl.isHiding)
        {
            state = State.ReturnToPatrol;
            return;
        }

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= chaseRadius)
        {
            lastKnownPos = player.position;
            MoveTowards(player.position);
        }
        else
        {
            state = State.ReturnToPatrol;
        }
    }

    void ReturnToPatrol()
    {
        MoveTowards(lastKnownPos);
        if (Vector2.Distance(transform.position, lastKnownPos) < 0.2f)
            state = State.Patrol;
    }

    void MoveTowards(Vector3 targetPos)
    {
        Vector3 dir = (targetPos - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        // Optional: atur sprite flip berdasarkan dir.x
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}