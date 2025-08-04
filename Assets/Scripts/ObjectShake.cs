using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    public float radius = 40.0f;
    public float speed = 5.0f;

    // The point we are going around in circles
    private Vector2 basestartpoint;

    // Destination of our current move
    private Vector2 destination;

    // Start of our current move
    private Vector2 start;

    // Current move's progress
    private float progress = 0.0f;

    RectTransform rectTransform;

    // Use this for initialization
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        basestartpoint = rectTransform.anchoredPosition;
        progress = 0.0f;

        PickNewRandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        // Update our progress to our destination
        progress += speed * Time.fixedDeltaTime; 

        // Check for the case when we overshoot or reach our destination
        if (progress >= 1.0f)
        {
            progress = 0f;
            PickNewRandomDestination();
        }

        // Update out position based on our start postion, destination and progress.
        rectTransform.anchoredPosition = Vector2.Lerp(basestartpoint, destination, progress);
    }

    void PickNewRandomDestination()
    {
        // We add basestartpoint to the mix so that is doesn't go around a circle in the middle of the scene.
        destination = basestartpoint + (Random.insideUnitCircle * radius);
    }
}
