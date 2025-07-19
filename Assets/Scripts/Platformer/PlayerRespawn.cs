using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Random Spawn Area")]
    [Tooltip("The maximum and minimum x-distance from this transform, for the player to respawn in.")]
    [SerializeField] private float xDistance;
    [Header("Assign Player")]
    [SerializeField] private GameObject player; 
    public void Respawn()
    {
        // Pick a random x-position from this transform's position, plus/minus the xDistance.
        float ranX = Random.Range(transform.position.x + xDistance, transform.position.x - xDistance);

        // Spawn player in new location.
        player.transform.position = new Vector2(ranX, transform.position.y);
    }
}
