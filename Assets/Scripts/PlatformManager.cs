using UnityEngine;
using System.Collections.Generic;
using LearningLadders.EventSystem;
using static Unity.Collections.AllocatorManager;

namespace LearningLadders
{
    public class PlatformManager : MonoBehaviour
    {
        [Header("Joint Attributes")]
        [Tooltip("Frequency of spring oscillating when game objects approach the separation distance.")]
        [SerializeField] private float jointFrequency = 1.5f;
        [Tooltip("Degree of supressing oscillation, from 0 to 1, higher = less movement.")]
        [SerializeField] private float jointDampingRatio = 0.5f; 
        [Tooltip("Enable collision between connected game objects.")]
        [SerializeField] private bool jointEnableCollision = false; 
        [Tooltip("How much force is required to perform the selected break action.")]
        [SerializeField] private float jointBreakForce; 
        [Tooltip("How much torque is required to perform the selected break action.")]
        [SerializeField] private float jointTorqueForce; 
        [Tooltip("Enable to automatically set the anchor location for the other object a joint connects to.")]
        [SerializeField] private bool jointAutoConfigureConnectedAnchor;

        [SerializeField] private IntEvent onStackConnectionEvent;

        private static Dictionary<int, StackableObject> stackableObjects = new();

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

            var joint = newObj.AddComponent<FixedJoint2D>();
            joint.connectedBody = collidedRb;
            joint.frequency = jointFrequency;
            joint.dampingRatio = jointDampingRatio;
            joint.autoConfigureConnectedAnchor = jointAutoConfigureConnectedAnchor;
            joint.breakForce = jointBreakForce;
            joint.breakTorque = jointTorqueForce;
            joint.breakAction = JointBreakAction2D.Destroy;
            joint.enableCollision = jointEnableCollision;

            int blockY = Mathf.FloorToInt(stackable.transform.position.y);
            onStackConnectionEvent.Invoke(blockY);

            stackable.MarkConnected();
        }
    }
}