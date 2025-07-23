using UnityEngine;

public class BackgroundObjects : MonoBehaviour
{
    [Header("Spawn Positions")]
    [SerializeField] private RectTransform[] leftSpawnPos;
    [SerializeField] private RectTransform[] rightSpawnPos;

    [Header("Flying Objects Prefabs")]
    [SerializeField] private GameObject[] lowTierObjects; // First objects that can spawn randomly at low level heights
    [SerializeField] private GameObject[] midTierObjects; // Objects that can spawn at a medium height (could be a range of 5 - 10m)
    [SerializeField] private GameObject[] highTierObjects; // Objects that can spawn at a tall height (could be 10+m).

    [Header("Spawn Variables")]
    [SerializeField] private float spawnInterval;
    [SerializeField] private float speed;

    private float spawnTimer = 0; 
    private int currentTier = 0; // 0 = low, 1 = mid, 2 = high

    private Vector3 startPos;
    private Vector3 endPos;

    private GameObject spawnedObject;

    private bool canMove = false;
    private float calculatedSpeed;

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        calculatedSpeed = speed * Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnObject();
            spawnTimer = 0f;
        }

         MoveObject();
    }

    /// <summary>
    /// Decide if the object spawns left or right, check what the current tier is.
    /// Spawn the appropriate object at the start position and set canMove to true.
    /// </summary>
    private void SpawnObject()
    {
        Destroy(spawnedObject);
        canMove = false;
        int isLeft = Random.Range(0, 2);

        if(isLeft == 0) // 0 = object spawns left, 1 = object spawns right.
        {
            startPos = leftSpawnPos[Random.Range(0, leftSpawnPos.Length)].position;
            endPos = rightSpawnPos[Random.Range(0, rightSpawnPos.Length)].position;
        }
        else
        {
            startPos = rightSpawnPos[Random.Range(0, rightSpawnPos.Length)].position;
            endPos = leftSpawnPos[Random.Range(0, leftSpawnPos.Length)].position;
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

        canMove = true;
    }

    /// <summary>
    /// Change current tier of background objects.
    /// </summary>
    /// <param name="tier"></param>
    public void ChangeTier(int tier)
    {
        currentTier += tier;
        if (currentTier <= 0) currentTier = 0;
        if(currentTier >= 2) currentTier = 2;
    }

    /// <summary>
    /// Move background object towards the end position.
    /// </summary>
    private void MoveObject()
    {
        if(!canMove) return;
        spawnedObject.transform.position = Vector3.MoveTowards(spawnedObject.transform.position, endPos, calculatedSpeed);
    }
}