
using UnityEngine;

public class HearingDetection : MonoBehaviour
{
    [Header("Hearing")]
    public Transform player;
    public float hearingDistance = 15f;

    private bool playerHeard = false;

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance <= hearingDistance && !playerHeard)
        {
            playerHeard = true;
            Debug.Log(gameObject.name + " heard the player!");
        }
        else if (distance > hearingDistance)
        {
            playerHeard = false;
        }
    }
}
