using UnityEngine;

public class LevelObjectSpawnScript : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public float spawnInterval = 1f;
    public float spawnRangeX = 8f;  //How far left/right objects can spawn

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
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
