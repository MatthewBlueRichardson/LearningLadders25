using LearningLadders.EventSystem;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    private AreaEffector2D _effector;
    private string _windDirection;

    private void Awake()
    {
        _effector = GetComponent<AreaEffector2D>();
    }

    private void Update()
    {
        
    }
}