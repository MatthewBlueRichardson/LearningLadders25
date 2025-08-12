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
    [SerializeField] private int randomObjectInt;
    [Tooltip("Chance out of 10 that repItem will spawn instead")]
    [SerializeField] private float repItemChance;
    [SerializeField] private float maxSpawnRate;

    private float difficultyTimer = 0f;
    private Camera mainCam;

    private bool hasReachedMaxSpawnSpeed = false;

    private void Start()
    {
        mainCam = Camera.main;
        StartCoroutine(SpawnObject());
    }

    private void Update()
    {
        difficultyTimer += Time.deltaTime;

        if(difficultyTimer >= difficultyIncreaseTimer && hasReachedMaxSpawnSpeed == false)
        {
            spawnInterval -= 0.25f;
            difficultyTimer = 0f;
            if (spawnInterval <= maxSpawnRate)
            {
                spawnInterval = maxSpawnRate;
                hasReachedMaxSpawnSpeed = true;
            }
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
        float ySpawn = PlayerPrefs.GetInt("Score");

        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y + ySpawn + 10, 0f);
        randomObjectInt = Random.Range(1, 10);

        float yPos = 0.9f;
        Vector3 warningPos = new Vector3(randomX, yPos, 0);
        warningPos = mainCam.ViewportToWorldPoint(warningPos);
        warningPos.x = randomX;
        warningPos.z = 0;

        GameObject newSpawnWarning = Instantiate(spawnWarning, warningPos, Quaternion.identity, transform);
        yield return new WaitForSeconds(spawnInterval);
        Destroy(newSpawnWarning);

        if (randomObjectInt <= repItemChance)
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
