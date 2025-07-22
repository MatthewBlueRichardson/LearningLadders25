using LearningLadders.EventSystem;
using UnityEngine;

public class ReputationItem : MonoBehaviour
{
    [SerializeField] private float repRestore = 15f;
    [SerializeField] private FloatEvent onIncreaseReputation;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            //Updates rep in ReputationScript
            onIncreaseReputation.Invoke(repRestore);
            Destroy(gameObject);
        }
    }
}
