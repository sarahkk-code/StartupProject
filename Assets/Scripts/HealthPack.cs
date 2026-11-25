
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ManageNPC2 npcHealth = other.GetComponent<ManageNPC2>();

        if (npcHealth != null)
        {
            npcHealth.setHealth(100);
            Debug.Log("NPC collected the health pack!");

            Destroy(gameObject);
        }
    }
}
