
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected)
            return;

        if (!other.CompareTag("Player"))
            return;

        collected = true;

        Debug.Log("Collected ONE sphere: " + gameObject.name);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore();
        }

        Destroy(gameObject);
    }
}
