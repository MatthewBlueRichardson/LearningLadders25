using LearningLadders.EventSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] VoidEvent onPauseGameEvent;
    [SerializeField] VoidEvent onUnpauseGameEvent;

    private bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) onPauseGameEvent.Invoke(new Empty());
            else onUnpauseGameEvent.Invoke(new Empty());
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
}