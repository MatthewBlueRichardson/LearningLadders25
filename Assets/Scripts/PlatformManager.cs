using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private GameObject[] fallingObjects;
    private GameObject _lastObjectSpawned;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // TODO: remove after testing!
        {
            SpawnRandomObject();
        }
    }

    /// <summary>
    /// This is placeholder code until the spawner is in.
    /// </summary>
    private void SpawnRandomObject() // TODO: remove after testing!
    {
        // Spawn random object from fallingObjects array.
        _lastObjectSpawned = Instantiate(fallingObjects[Random.Range(0, fallingObjects.Length)], new Vector3(0, 0, 0), Quaternion.identity);
    }
}