using UnityEngine;
using LearningLadders.EventSystem;
using LearningLadders.Audio;

namespace LearningLadders
{
    public class StackableObject : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private IntEvent onStackConnectionEvent;
        [SerializeField] private VoidEvent onStackBroken;
        [SerializeField] private VoidEvent onGameOverEvent;
        [SerializeField] private FloatEvent onDamageReputation;
        [SerializeField] private AudioClipSOEvent onPlaySfxEvent;

        [Header("Game Objects")]
        [SerializeField] private GameObject repTextObject;

        [Header("Audio Clip SOs")]
        [SerializeField] private AudioClipSO objectCollisionSfx;

        [Header("Variables")]
        [SerializeField] private float invulnerabilityPeriod;

        private bool canConnect = false;
        private bool isConnected = false;
        private GameObject lastPlatformPart;

        public int ID {  get; private set; }

        public float repDamage = -10f;

        private void Awake()
        {
            ID = GetInstanceID();
            PlatformManager.RegisterStackable(ID, this);
        }

        private void Update()
        {
            if(!canConnect) invulnerabilityPeriod -= Time.deltaTime;

            if (invulnerabilityPeriod < 0) canConnect = true;
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
                //Spawns rep damage number text
                Instantiate(repTextObject, transform.position, Quaternion.Euler(0, 0, 0));
                onDamageReputation.Invoke(repDamage);
                Destroy(gameObject);
            }

            if (isConnected || !canConnect) return;

            if (collision.collider.CompareTag("PartOfPlatform") || collision.collider.CompareTag("Stackable"))
            {
                lastPlatformPart = collision.collider.gameObject;
                onStackConnectionEvent.Invoke(ID);
                onPlaySfxEvent.Invoke(objectCollisionSfx);
            }
        }

        void OnJointBreak2D(Joint2D brokenJoint)
        {
            onStackBroken.Invoke(new Empty()); 
        }

        public GameObject GetLastPlatformPart() => lastPlatformPart;

        public void MarkConnected() => isConnected = true;
    }
}

