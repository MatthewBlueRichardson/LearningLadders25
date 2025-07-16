using UnityEngine;
using LearningLadders.EventSystem;

namespace LearningLadders
{
    public class StackableObject : MonoBehaviour
    {
        [SerializeField] private IntEvent onStackConnection;

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

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (isConnected) return;

            if (collision.collider.CompareTag("PartOfPlatform") || collision.collider.CompareTag("Stackable"))
            {
                Debug.Log("Tag matches!");
                //isConnected = true;
                lastPlatformPart = collision.collider.gameObject;
                onStackConnection.Invoke(ID);
            }
        }

        public GameObject GetLastPlatformPart() => lastPlatformPart;

        public void MarkConnected() => isConnected = true;
    }
}

