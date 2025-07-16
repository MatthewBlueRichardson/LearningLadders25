using UnityEngine;
using LearningLadders.EventSystem;

namespace LearningLadders
{
    public class FallingObject : MonoBehaviour
    {
        [SerializeField] private IntEvent onStackConnection;

        private bool isConnected = false;
        private GameObject lastPlatformPart;

        public int ID {  get; private set; }

        private void Awake()
        {
            ID = GetInstanceID();
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
        
        }
    }
}

