
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ControlNPC3 : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] points;
    public float waitTime = 1f;

    [Header("Vision")]
    public Transform player;
    public float sightDistance = 10f;
    public float fleeDistance = 4f;

    [Header("Health Pack")]
    public Transform healthPack;

    [Header("Shooting")]
    public float shootingDistance = 10f;
    public float shootingCooldown = 2f;

    private NavMeshAgent agent;
    private Animator animator;
    private ManageNPC2 healthManager;

    private int currentPoint = 0;
    private bool searchingForHealth = false;
    private bool dying = false;
    private bool ambushing = false;
    private bool fleeing = false;

    private float nextShotTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        healthManager = GetComponent<ManageNPC2>();

        if (agent == null || animator == null ||
            healthManager == null || points == null ||
            points.Length == 0 || player == null)
        {
            Debug.LogError("NPC3 is missing a required component or reference.");
            enabled = false;
            return;
        }

        StartCoroutine(ControlMovement());
    }

    void Update()
    {
        if (dying) return;

        if (healthManager.getHealth() <= 0)
        {
            dying = true;
            agent.isStopped = true;
            return;
        }

        if (healthManager.getHealth() < 20 && !searchingForHealth)
        {
            if (healthPack != null)
            {
                ambushing = false;
                fleeing = false;
                StartCoroutine(FindHealthPack());
            }
            return;
        }

        if (searchingForHealth) return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        // Simple distance-based detection
        bool canSeePlayer = distance <= sightDistance;

        if (canSeePlayer && distance <= fleeDistance)
        {
            if (!fleeing)
            {
                fleeing = true;
                ambushing = false;
                animator.SetTrigger("startToFlee");
                Debug.Log("NPC3 is fleeing from the player!");
            }

            Vector3 direction =
                (transform.position - player.position).normalized;

            Vector3 destination =
                transform.position + direction * 25f;

            agent.isStopped = false;
            agent.SetDestination(destination);
        }
        else if (canSeePlayer)
        {
            fleeing = false;

            if (!ambushing)
            {
                ambushing = true;
                animator.SetTrigger("startToAmbush");
                Debug.Log("NPC3 is ambushing the player!");
            }

            agent.isStopped = false;
            agent.SetDestination(player.position);

            ShootPlayer(distance);
        }
        else
        {
            if (ambushing || fleeing)
            {
                ambushing = false;
                fleeing = false;

                animator.ResetTrigger("startToAmbush");
                animator.ResetTrigger("startToFlee");

                Debug.Log("NPC3 returned to patrol.");
            }
        }
    }

    void ShootPlayer(float distance)
    {
        if (distance > shootingDistance) return;
        if (Time.time < nextShotTime) return;

        nextShotTime = Time.time + shootingCooldown;

        Vector3 direction =
            (player.position - transform.position).normalized;

        Debug.DrawRay(
            transform.position + Vector3.up,
            direction * shootingDistance,
            Color.red,
            1f
        );

        Debug.Log("NPC3 fired at the player!");
    }

    IEnumerator ControlMovement()
    {
        while (!dying)
        {
            if (searchingForHealth || ambushing || fleeing)
            {
                yield return null;
                continue;
            }

            agent.isStopped = false;
            agent.SetDestination(points[currentPoint].position);

            while (agent.pathPending ||
                   agent.remainingDistance > agent.stoppingDistance)
            {
                if (searchingForHealth || ambushing ||
                    fleeing || dying)
                    break;

                yield return null;
            }

            if (dying) yield break;

            if (searchingForHealth || ambushing || fleeing)
                continue;

            agent.isStopped = true;
            yield return new WaitForSeconds(waitTime);

            currentPoint = (currentPoint + 1) % points.Length;
        }
    }

    IEnumerator FindHealthPack()
    {
        searchingForHealth = true;

        Debug.Log("NPC3 is searching for health!");

        agent.isStopped = false;
        agent.SetDestination(healthPack.position);

        while (!dying && healthPack != null &&
               Vector3.Distance(
                   transform.position,
                   healthPack.position
               ) > 1.5f)
        {
            agent.SetDestination(healthPack.position);
            yield return null;
        }

        if (!dying && healthPack != null)
        {
            agent.isStopped = true;
            healthManager.setHealth(100);

            Destroy(healthPack.gameObject);
            healthPack = null;

            Debug.Log("NPC3 collected the health pack!");
        }

        searchingForHealth = false;
    }
}
