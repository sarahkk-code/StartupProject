
using UnityEngine;
using UnityEngine.AI;

public class NPCRandomPath : MonoBehaviour
{
    public float radius = 15f;
    public float waitTime = 2f;

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!agent.pathPending &&
            agent.remainingDistance < 0.5f &&
            timer >= waitTime)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomPoint = transform.position +
            Random.insideUnitSphere * radius;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            timer = 0;
        }
    }
}
