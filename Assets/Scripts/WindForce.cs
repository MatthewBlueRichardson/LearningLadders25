using LearningLadders.EventSystem;
using UnityEngine;
using TMPro;

public class WindForce : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI windForceText;
    [SerializeField] private TextMeshProUGUI windDirectionText;

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
        effector.forceAngle = 180; // Set to left
        effector.forceMagnitude = Random.Range(7, 21);

        windDirection = "Wind Direction: West!";
        windForce = "Wind Force: " + effector.forceMagnitude.ToString() + " mph!";

        isRight = false;
        windDirectionText.text = windDirection;
        windForceText.text = windForce;
    }

    private void SetWindForceToRight()
    {
        effector.forceAngle = 0; // Set to left
        effector.forceMagnitude = Random.Range(7, 21);

        windDirection = "Wind Direction: East!";
        windForce = "Wind Force: " + effector.forceMagnitude.ToString() + " mph!";

        isRight = true;
        windDirectionText.text = windDirection;
        windForceText.text = windForce;
    }
}