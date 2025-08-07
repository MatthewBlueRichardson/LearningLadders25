using LearningLadders.EventSystem;
using UnityEngine;
using UnityEngine.UI;

public class RisingKillbox : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 0.6f;

    [SerializeField] private BoolEvent enterEvent;
    [SerializeField] private BoolEvent exitEvent;

    [SerializeField] private Image repBar;

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enterEvent.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exitEvent.Invoke(false);
        }
    }
}
