using UnityEngine;

public class BackgroundObjects : MonoBehaviour
{
    [Header("Spawn Positions")]
    [SerializeField] private Vector3[] leftSpawnPos;
    [SerializeField] private Vector3[] rightSpawnPos;

    [Header("Flying Objects Prefabs")]
    [SerializeField] private GameObject[] lowTierObjects; // First objects that can spawn randomly at low level heights
    [SerializeField] private GameObject[] midTierObjects; // Objects that can spawn at a medium height (could be a range of 5 - 10m)
    [SerializeField] private GameObject[] highTierObjects; // Objects that can spawn at a tall height (could be 10+m).

    private float spawnTimer;
    private float spawnInterval;

    private int currentTier = 0; // 0 = low, 1 = mid, 2 = high

    private Vector3 startPos;
    private Vector3 endPos;

    private GameObject spawnedObject;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnObject();
            spawnTimer = 0f;
        }
    }

    private void SpawnObject()
    {
        int isLeft = Random.Range(0, 2);

        if(isLeft == 0) // 0 = object spawns left, 1 = object spawns right.
        {
            startPos = leftSpawnPos[Random.Range(0, leftSpawnPos.Length)];
            endPos = rightSpawnPos[Random.Range(0, rightSpawnPos.Length)];
        }
        else
        {
            startPos = rightSpawnPos[Random.Range(0, rightSpawnPos.Length)];
            endPos = leftSpawnPos[Random.Range(0, leftSpawnPos.Length)];
        }

        switch(currentTier)
        {
            case 0: // Instantiate at start pos, set path to end pos.
                spawnedObject = Instantiate(lowTierObjects[Random.Range(0, lowTierObjects.Length)], startPos, Quaternion.identity);
                break;

            case 1:
                spawnedObject = Instantiate(midTierObjects[Random.Range(0, midTierObjects.Length)], startPos, Quaternion.identity);
                break;

            case 2:
                spawnedObject = Instantiate(highTierObjects[Random.Range(0, highTierObjects.Length)], startPos, Quaternion.identity);
                break;
        }

        // Set path of object to end pos!
        MoveObject();
    }

    public void ChangeTier(int tier)
    {
        currentTier += tier;
        Debug.Log("New tier of objects! " + currentTier);
        if (currentTier <= 0) currentTier = 0;
        if(currentTier >= 2) currentTier = 2;
    }

    private void MoveObject()
    {
        //spawnedObject.transform.position = endPos;
    }
}