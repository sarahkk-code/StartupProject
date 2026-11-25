
using UnityEngine;

public class NPCShooting : MonoBehaviour
{
    public Transform player;
    public float shootingDistance = 10f;
    public float shootingCooldown = 2f;

    private float nextShotTime;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(
            transform.position, player.position
        );

        if (distance <= shootingDistance &&
            Time.time >= nextShotTime)
        {
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
    }
}
