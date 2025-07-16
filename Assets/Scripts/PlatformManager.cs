using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Animations.Rigging;

namespace LearningLadders
{
    public class PlatformManager : MonoBehaviour
    {
        [Header("Falling Object Prefabs")]
        [SerializeField] private GameObject[] fallingObjects;

        [Header("Game Objects")]
        [SerializeField] private GameObject rig;
        [SerializeField] private GameObject dampedTransObject;

        private GameObject _previousSpawnedObject;
        private GameObject _newObject;

        private void Start()
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
            _newObject = Instantiate(fallingObjects[Random.Range(0, fallingObjects.Length)], new Vector3(0, 4, 0), Quaternion.identity);
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
    }
}