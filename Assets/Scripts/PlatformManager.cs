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

        [Header("Game Objects")]
        [SerializeField] private GameObject rig;
        [SerializeField] private GameObject dampedTransObject;

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

        /*public void OnCollisionEnter2D(Collision2D collision) // I will have to replace this in the future with something else, needs to check if new object touches anything from the stack rather than the platform!
        {
            Debug.Log("Collision!");

            collision.transform.SetParent(gameObject.transform, true);
            var newDampedTrans = Instantiate(dampedTransObject, rig.transform);
            DampedTransform dampedTransComponent = newDampedTrans.GetComponent<DampedTransform>();

            dampedTransComponent.data.sourceObject = _previousSpawnedObject.transform;
            dampedTransComponent.data.constrainedObject = _newObject.transform;

            _previousSpawnedObject = _newObject;
        }*/

        public static void RegisterStackable(int id, StackableObject obj)
        {
            if(!stackableObjects.ContainsKey(id)) 
                stackableObjects.Add(id, obj);
        }

        public static void UnregisterStackable(int id)
        {
            stackableObjects.Remove(id);
        }

        public void ConnectStackObject(int id)
        {
            if (!stackableObjects.TryGetValue(id, out var stackable)) return;

            GameObject newObj = stackable.gameObject;
            GameObject collidedObj = stackable.GetLastPlatformPart();

            //var connectToObj = _lastConnectedObject != null ? _lastConnectedObject : gameObject;

            //var hitObj = stackable.GetLastPlatformPart();
            //if(newObj == null || hitObj == null) return;

            if (newObj == null || collidedObj == null) return;

            Rigidbody2D newRb = newObj.GetComponent<Rigidbody2D>();
            Rigidbody2D collidedRb = collidedObj.GetComponent<Rigidbody2D>();

            //var hitRb = hitObj.GetComponent<Rigidbody2D>();

            if(newRb == null || collidedRb == null) return;

            //Debug.Log($"Connecting object {id} to platform part {connectRb.name}");

            var joint = newObj.AddComponent<SpringJoint2D>();
            joint.connectedBody = collidedRb;
            joint.frequency = jointFrequency;
            joint.dampingRatio = jointDampingRatio;
            joint.autoConfigureDistance = jointAutoConfigureDistance;
            joint.distance = jointDistance;
            joint.enableCollision = jointEnableCollision;

            //_lastConnectedObject = newObj;
            stackable.MarkConnected();
        }
    }
}