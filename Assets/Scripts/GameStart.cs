using LearningLadders.Audio;
using LearningLadders.EventSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public Animator pisaAnimation;
    [SerializeField] private float animationTime = 2f;
    [SerializeField] private AudioClipSO audioClip;
    [SerializeField] private AudioClipSOEvent onPlaySfxEvent;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        onPlaySfxEvent.Invoke(audioClip);
        pisaAnimation.SetTrigger("Fall");
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(animationTime);
        SceneManager.LoadScene("MainGame");
    }
}
