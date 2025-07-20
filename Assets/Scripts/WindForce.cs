using LearningLadders.EventSystem;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    private AreaEffector2D effector;
    private string windDirection;
    private string windForce;
    private bool isRight;
    private float timer;
    private float interval;

    private void Awake()
    {
        effector = GetComponent<AreaEffector2D>();
    }

    private void Start()
    {
        SetWindForceToLeft();
        interval = 10f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (isRight && timer >= interval)
        {
            SetWindForceToLeft();
            timer = 0;
            interval = Random.Range(8, 15);
        }

        if (!isRight && timer >= interval) 
        {
            SetWindForceToRight();
            timer = 0;
            interval = Random.Range(8, 15);
        }
    }

    private void SetWindForceToLeft()
    {
        windDirection = "Wind Direction: West!";
        effector.forceAngle = 180; // Set to left
        windForce = effector.forceMagnitude.ToString() + " mph!";
        isRight = false;
        Debug.Log("Setting wind to left!" + windDirection + windForce);
    }

    private void SetWindForceToRight()
    {
        windDirection = "Wind Direction: East!";
        effector.forceAngle = 0; // Set to left
        windForce = "Wind Force: " + effector.forceMagnitude.ToString() + " mph!";
        isRight = true;
        Debug.Log("Setting wind to right!" + windDirection + windForce);
    }
}