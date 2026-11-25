
using UnityEngine;

public class HealthCareSpawner : MonoBehaviour
{
    public GameObject healthPackPrefab;
    public Vector3 spawnAreaMin = new Vector3(-20, 1, -20);
    public Vector3 spawnAreaMax = new Vector3(20, 1, 20);
    public float spawnInterval = 15f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnHealthPack), 2f, spawnInterval);
    }

    void SpawnHealthPack()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            spawnAreaMin.y,
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );

        Instantiate(
            healthPackPrefab,
            randomPosition,
            Quaternion.identity
        );

        Debug.Log("A health pack spawned!");
    }
}
