using LearningLadders.EventSystem;
using UnityEngine;

public class ReputationItem : MonoBehaviour
{
    [SerializeField] private float repRestore = 15f;
    [SerializeField] private FloatEvent onIncreaseReputation;
    [SerializeField] private VoidEvent onPickedUp;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            //Updates rep in ReputationScript
            onIncreaseReputation.Invoke(repRestore);
            onPickedUp.Invoke(new Empty());
            Destroy(gameObject);
        }
    }
}
