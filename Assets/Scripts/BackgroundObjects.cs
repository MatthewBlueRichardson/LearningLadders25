using UnityEngine;

public class BackgroundObjects : MonoBehaviour
{
    [Header("Spawn Positions")]
    [SerializeField] private Vector2[] leftSpawnPos;
    [SerializeField] private Vector2[] rightSpawnPos;

    [Header("Flying Objects Prefabs")]
    [SerializeField] private GameObject[] lowTierObjects; // First objects that can spawn randomly at low level heights
    [SerializeField] private GameObject[] midTierObjects; // Objects that can spawn at a medium height (could be a range of 5 - 10m)
    [SerializeField] private GameObject[] highTierObjects; // Objects that can spawn at a tall height (could be 10+m).

    private float spawnTimer;
    private float spawnInterval;

    private int currentTier = 0; // 0 = low, 1 = mid, 2 = high

    private Vector2 startPos;
    private Vector2 endPos;

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
                Instantiate(lowTierObjects[Random.Range(0, lowTierObjects.Length)]);
                break;

            case 1:
                break;

            case 2:
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

    }
}