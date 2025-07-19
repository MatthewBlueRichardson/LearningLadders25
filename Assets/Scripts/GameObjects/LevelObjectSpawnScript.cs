using UnityEngine;

public class LevelObjectSpawnScript : MonoBehaviour
{
    [Header("Objects To Spawn")]
    [SerializeField] private GameObject[] objectsToSpawn;

    [Header("Spawn Variables")]
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float spawnRangeX = 8f;  //How far left/right objects can spawn
    [SerializeField] private float difficultyIncreaseTimer = 10f; // After how much time to increase the difficulty.

    private float spawnTimer = 0f;
    private float difficultyTimer = 0f;

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnObject();
            spawnTimer = 0f;
        }

        if(difficultyTimer >= difficultyIncreaseTimer)
        {
            spawnInterval -= 0.25f;
            difficultyTimer = 0f;
            if (spawnInterval <= 1.5f) spawnInterval = 1.5f;
        }
    }

    void SpawnObject()
    {
        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        GameObject prefabToSpawn = objectsToSpawn[randomIndex];

        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0f);

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}
