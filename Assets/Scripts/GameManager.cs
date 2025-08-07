using LearningLadders.EventSystem;
using UnityEngine;
using LearningLadders.Audio;

public class GameManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private VoidEvent onPauseGameEvent;
    [SerializeField] private VoidEvent onUnpauseGameEvent;
    [SerializeField] private AudioClipSOEvent onPlaySfxEvent;
    [SerializeField] private AudioClipSO startGame;

    private bool isPaused;

    private void Start()
    {
        UnpauseGame();
        onPlaySfxEvent.Invoke(startGame);
    }

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