using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public LayerMask groundLayer;
    public PlatformerMovement pMovement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((groundLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            pMovement.ResetJump();
        }
    }
}
