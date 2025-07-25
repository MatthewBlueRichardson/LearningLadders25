using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Random Spawn Area")]
    [Tooltip("The maximum and minimum x-distance from this transform, for the player to respawn in.")]
    [SerializeField] private float xDistance;
    [Header("Assign Player")]
    [SerializeField] private GameObject player;

    private int previousHeight;
    private int minY;

    private void Start()
    {
        previousHeight = 0;
        minY = (int)transform.position.y;
    }

    public void Respawn()
    {
        // Pick a random x-position from this transform's position, plus/minus the xDistance.
        float ranX = Random.Range(transform.position.x + xDistance, transform.position.x - xDistance);

        // Spawn player in new location.
        player.transform.position = new Vector2(ranX, transform.position.y);
    }

    // This function adds height to the respawn position, based on the height of the last stackable.
    public void UpdateSpawnHeight(int height)
    {
        if(height < -1 && height < previousHeight)
        {
            return;
        }
             
        transform.position = new Vector2(transform.position.x, minY + height);

        previousHeight = height;
    }
}
