
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ControlNPC2 : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] points;
    public float waitTime = 1f;

    [Header("Vision")]
    public Transform player;
    public float sightDistance = 10f;
    public float fieldOfView = 60f;

    private NavMeshAgent agent;
    private Animator animator;
    private int currentPoint = 0;
    private bool playerDetected = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        Debug.Log(
            "Checking NPC: " + gameObject.name +
            " | Points: " + (points != null ? points.Length.ToString() : "NULL") +
            " | Agent: " + (agent != null) +
            " | Animator: " + (animator != null) +
            " | Player: " + (player != null)
        );

        if (points == null || points.Length == 0 ||
            agent == null || animator == null || player == null)
        {
            Debug.LogError("Missing reference on: " + gameObject.name);
            enabled = false;
            return;
        }

        agent.isStopped = true;
        StartCoroutine(Patrol());
    }

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        if (player == null)
            return;

        Vector3 origin = transform.position + Vector3.up * 1.5f;
        Vector3 target = player.position + Vector3.up;

        Vector3 direction = target - origin;
        float distance = direction.magnitude;

        float angle = Vector3.Angle(
            transform.forward,
            direction.normalized
        );

        if (distance <= sightDistance &&
            angle <= fieldOfView / 2f)
        {
            RaycastHit[] hits = Physics.RaycastAll(
                origin,
                direction.normalized,
                sightDistance
            );

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform == player ||
                    hit.transform.IsChildOf(player))
                {
                    if (!playerDetected)
                    {
                        Debug.Log(gameObject.name + " spotted the player!");
                        playerDetected = true;
                    }

                    return;
                }
            }
        }

        playerDetected = false;
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            animator.SetTrigger("startPatrol");

            agent.isStopped = false;
            agent.SetDestination(points[currentPoint].position);

            while (agent.pathPending ||
                   agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            agent.isStopped = true;

            yield return new WaitForSeconds(waitTime);

            currentPoint = (currentPoint + 1) % points.Length;
        }
    }
}
