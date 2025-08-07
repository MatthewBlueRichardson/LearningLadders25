using LearningLadders.EventSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindForce : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI windForceText;

    [SerializeField] private Image windArrow;

    [SerializeField] private int windTier;

    [SerializeField] private Animator windAnim;

    [SerializeField] private IntEvent windTierEvent;
    [SerializeField] private BoolEvent flipCanvasEvent;

    private AreaEffector2D effector;
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

        windAnim.SetTrigger("WindChange");
        windArrow.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        windForce = effector.forceMagnitude.ToString() + " mph";

        isRight = false;
        windForceText.text = windForce;
        flipCanvasEvent.Invoke(isRight);

        if (effector.forceMagnitude <= 12)
        {
            windTier = 1;
            windTierEvent.Invoke(windTier);
        }
        else if (effector.forceMagnitude <= 17)
        {
            windTier = 2;
            windTierEvent.Invoke(windTier);
        }
        else if (effector.forceMagnitude <= 21)
        {
            windTier = 3;
            windTierEvent.Invoke(windTier);
        }

    }

    private void SetWindForceToRight()
    {
        effector.forceAngle = 0; // Set to left
        effector.forceMagnitude = Random.Range(7, 21);

        windAnim.SetTrigger("WindChange");
        windArrow.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        windForce = effector.forceMagnitude.ToString() + " mph";

        isRight = true;
        windForceText.text = windForce;
        flipCanvasEvent.Invoke(isRight);

        if (effector.forceMagnitude <= 12)
        {
            windTier = 1;
            windTierEvent.Invoke(windTier);
        }
        else if (effector.forceMagnitude <= 17)
        {
            windTier = 2;
            windTierEvent.Invoke(windTier);
        }
        else if (effector.forceMagnitude <= 21)
        {
            windTier = 3;
            windTierEvent.Invoke(windTier);
        }

        
    }
}