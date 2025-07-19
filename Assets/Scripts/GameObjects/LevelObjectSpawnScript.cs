using UnityEngine;

public class LevelObjectSpawnScript : MonoBehaviour
{
    [Header("Objects To Spawn")]
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private GameObject repItem;

    [Header("Spawn Variables")]
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnRangeX;  //How far left/right objects can spawn
    [SerializeField] private float randomObjectFloat;
    [SerializeField] private float repItemChance;

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
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0f);
        randomObjectFloat = Random.Range(1f, 10f);

        if (randomObjectFloat > repItemChance)
        {
            Instantiate(repItem, spawnPos, Quaternion.identity);
        }

        else
        {
            int randomIndex = Random.Range(0, objectsToSpawn.Length);
            GameObject prefabToSpawn = objectsToSpawn[randomIndex];
            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        }     
    }
}
