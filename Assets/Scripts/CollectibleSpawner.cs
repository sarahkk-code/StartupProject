
using UnityEngine;
using TMPro;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public TMP_Dropdown collectibleDropdown;
    public float spawnRadius = 8f;

    private void Start()
    {
        if (collectibleDropdown != null)
        {
            collectibleDropdown.onValueChanged.AddListener(
                OnDropdownChanged
            );
        }

        SpawnCollectibles();
    }

    private void OnDestroy()
    {
        if (collectibleDropdown != null)
        {
            collectibleDropdown.onValueChanged.RemoveListener(
                OnDropdownChanged
            );
        }
    }

    private void OnDropdownChanged(int value)
    {
        SpawnCollectibles();
    }

    public void SpawnCollectibles()
    {
        if (collectiblePrefab == null)
        {
            Debug.LogError("Collectible Prefab is not assigned!");
            return;
        }

        int count = 3;

        if (collectibleDropdown != null)
        {
            string selected = collectibleDropdown.options[
                collectibleDropdown.value
            ].text;

            if (selected.Contains("10"))
                count = 10;
            else if (selected.Contains("5"))
                count = 5;
        }

        // Remove existing collectibles.
        GameObject[] existing =
            GameObject.FindGameObjectsWithTag("Collectible");

        foreach (GameObject item in existing)
        {
            Destroy(item);
        }

        // Tell GameManager how many are required to finish.
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetCollectiblesRequired(count);
        }

        // Spawn the selected number.
        for (int i = 0; i < count; i++)
        {
            Vector3 position = transform.position + new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0.5f,
                Random.Range(-spawnRadius, spawnRadius)
            );

            Instantiate(
                collectiblePrefab,
                position,
                Quaternion.identity
            );
        }

        Debug.Log("Spawned " + count +
                  " collectibles. Required score: " + count);
    }
}
