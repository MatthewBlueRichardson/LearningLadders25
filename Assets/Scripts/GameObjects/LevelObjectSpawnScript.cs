using UnityEngine;
using System.Collections;

public class LevelObjectSpawnScript : MonoBehaviour
{
    [Header("Objects To Spawn")]
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private GameObject repItem;
    [SerializeField] private GameObject spawnWarning;

    [Header("Spawn Variables")]
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float spawnRangeX = 8f;  //How far left/right objects can spawn
    [SerializeField] private float difficultyIncreaseTimer = 10f; // After how much time to increase the difficulty.
    [SerializeField] private float randomObjectFloat;
    [Tooltip("Chance out of 10 that repItem will spawn instead")]
    [SerializeField] private float repItemChance;

    private float difficultyTimer = 0f;

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private void Update()
    {
        difficultyTimer += Time.deltaTime;

        if(difficultyTimer >= difficultyIncreaseTimer)
        {
            spawnInterval -= 0.25f;
            difficultyTimer = 0f;
            if (spawnInterval <= 1.5f) spawnInterval = 1.5f;
        }
    }

    /// <summary>
    /// Decides a spawn location and an object to spawn. It spawns
    /// a warning a bit below the spawn position and deletes the warning
    /// sign before spawning the chosen object.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnObject()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0f);
        randomObjectFloat = Random.Range(1f, 10f);

        Vector3 warningPos = new Vector3(randomX, transform.position.y - 4, 0f);
        GameObject newSpawnWarning = Instantiate(spawnWarning, warningPos, Quaternion.identity, transform);
        yield return new WaitForSeconds(3 * (spawnInterval/4));
        Destroy(newSpawnWarning);
        yield return new WaitForSeconds(spawnInterval/4);

        if (randomObjectFloat <= repItemChance)
        {
            Instantiate(repItem, spawnPos, Quaternion.identity);
        }

        else
        {
            int randomIndex = Random.Range(0, objectsToSpawn.Length);
            GameObject prefabToSpawn = objectsToSpawn[randomIndex];
            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        }     

        StartCoroutine(SpawnObject());
    }
}
