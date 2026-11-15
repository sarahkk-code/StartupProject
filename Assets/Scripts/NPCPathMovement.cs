
using UnityEngine;
using UnityEngine.AI;

public class NPCPathMovement : MonoBehaviour
{
    public Transform[] points;
    public float waitTime = 1f;

    private NavMeshAgent agent;
    private int currentPoint = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (points.Length > 0)
            agent.SetDestination(points[currentPoint].position);
    }

    void Update()
    {
        if (!agent.pathPending &&
            agent.remainingDistance < 0.5f)
        {
            currentPoint = (currentPoint + 1) % points.Length;
            agent.SetDestination(points[currentPoint].position);
        }
    }
}
