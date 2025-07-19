using UnityEngine;
using LearningLadders.EventSystem;

namespace LearningLadders
{
    public class StackableObject : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private IntEvent onStackConnectionEvent;
        [SerializeField] private VoidEvent onGameOverEvent;

        private bool isConnected = false;
        private GameObject lastPlatformPart;

        public int ID {  get; private set; }

        private void Awake()
        {
            ID = GetInstanceID();
            PlatformManager.RegisterStackable(ID, this);
        }

        private void OnDestroy()
        {
            PlatformManager.UnregisterStackable(ID);
        }

        /// <summary>
        /// When the stackable object collides with another object, it checks whether this object is already connected.
        /// If not, it then checks the tag of the object it collided with. If it meets the condition,
        /// it tracks the object it collided with and invokes the stack connection event and passes its own ID number.
        /// </summary>
        /// <param name="collision"></param>
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.CompareTag("GameOver"))
            {
                onGameOverEvent.Invoke(new Empty());
                Destroy(gameObject);
            }

            if (isConnected) return;

            if (collision.collider.CompareTag("PartOfPlatform") || collision.collider.CompareTag("Stackable"))
            {
                lastPlatformPart = collision.collider.gameObject;
                onStackConnectionEvent.Invoke(ID);
            }
        }

        public GameObject GetLastPlatformPart() => lastPlatformPart;

        public void MarkConnected() => isConnected = true;
    }
}

