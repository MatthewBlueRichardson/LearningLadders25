using UnityEngine;

public class ReputationItem : MonoBehaviour
{
    [SerializeField] private ReputationScript repScript;
    [SerializeField] private float repRestore = 15f;

    void Start()
    {
        if (repScript == null)
        {
            repScript = FindAnyObjectByType<ReputationScript>();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        repScript.currentRep += repRestore;
        Destroy(gameObject);
    }
}
