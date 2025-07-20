using LearningLadders.EventSystem;
using UnityEngine;

public class ReputationItem : MonoBehaviour
{
    [SerializeField] private float repRestore = 15f;
    [SerializeField] private FloatEvent onUpdateReputation;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //Updates rep in ReputationScript
            onUpdateReputation.Invoke(repRestore);
            Destroy(gameObject);
        }
    }
}
