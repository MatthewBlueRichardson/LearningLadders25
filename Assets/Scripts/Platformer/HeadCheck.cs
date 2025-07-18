using LearningLadders.EventSystem;
using UnityEngine;

public class HeadCheck : MonoBehaviour
{
    private VoidEventTrigger eventTrigger;

    private void Start()
    {
        eventTrigger = GetComponent<VoidEventTrigger>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This if-statement checks if the player character's head has been struck by a fallen stackable object.
        if (collision.gameObject.CompareTag("Stackable"))
        {
            Rigidbody2D rb = collision.attachedRigidbody;

            // If the stackable has a RigidBody, and is currently falling...
            if(rb != null && rb.linearVelocityY < -0.2f)
            {
                Debug.Log("Hit");
                eventTrigger.Trigger(); // Call OnCharacterCrushed UnityEvent.
            }
        }
    }
}
