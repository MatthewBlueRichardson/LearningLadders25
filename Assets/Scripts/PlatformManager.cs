using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;

namespace LearningLadders
{
    public class PlatformManager : MonoBehaviour
    {
        [Header("Falling Object Prefabs")]
        [SerializeField] private GameObject[] fallingObjectPrefabs;

        [Header("Joint Attributes")]
        [SerializeField] private float jointFrequency = 1.5f;
        [SerializeField] private float jointDampingRatio = 0.5f;
        [SerializeField] private bool jointAutoConfigureDistance = false;
        [SerializeField] private float jointDistance = 0f;
        [SerializeField] private bool jointEnableCollision = false;

        private GameObject _previousSpawnedObject;
        private GameObject _newObject;

        private static GameObject _lastConnectedObject;

        private static Dictionary<int, StackableObject> stackableObjects = new();

        private void Start() // TODO: remove after testing!
        {
            _previousSpawnedObject = gameObject;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S)) // TODO: remove after testing!
            {
                SpawnRandomObject();
            }
        }

        /// <summary>
        /// This is placeholder code until the spawner is in.
        /// </summary>
        private void SpawnRandomObject() // TODO: remove after testing!
        {
            _newObject = Instantiate(fallingObjectPrefabs[Random.Range(0, fallingObjectPrefabs.Length)], new Vector3(0, 4, 0), Quaternion.identity);
        }

        public static void RegisterStackable(int id, StackableObject obj)
        {
            if(!stackableObjects.ContainsKey(id)) 
                stackableObjects.Add(id, obj);
        }

        public static void UnregisterStackable(int id)
        {
            stackableObjects.Remove(id);
        }

        /// <summary>
        /// Tracks the two objects that need to be connected. Adds a joint component to the
        /// new object and sets the joint settings. It then turns the boolean "isConnected" from 
        /// the stackable object to true.
        /// </summary>
        /// <param name="id"></param>
        public void ConnectStackObject(int id)
        {
            if (!stackableObjects.TryGetValue(id, out var stackable)) return;

            GameObject newObj = stackable.gameObject;
            GameObject collidedObj = stackable.GetLastPlatformPart();

            if (newObj == null || collidedObj == null) return;

            Rigidbody2D newRb = newObj.GetComponent<Rigidbody2D>();
            Rigidbody2D collidedRb = collidedObj.GetComponent<Rigidbody2D>();

            if(newRb == null || collidedRb == null) return;

            var joint = newObj.AddComponent<SpringJoint2D>();
            joint.connectedBody = collidedRb;
            joint.frequency = jointFrequency;
            joint.dampingRatio = jointDampingRatio;
            joint.autoConfigureDistance = jointAutoConfigureDistance;
            joint.distance = jointDistance;
            joint.enableCollision = jointEnableCollision;

            stackable.MarkConnected();
        }
    }
}