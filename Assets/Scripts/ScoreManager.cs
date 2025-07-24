using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LearningLadders.EventSystem;
using LearningLadders.Audio;

public class ScoreManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClipSOEvent sfxEvent;
    [SerializeField] private AudioClipSO scoreSound;
    [SerializeField] private AudioClipSO scoreMilestoneSound;

    [SerializeField] private IntEvent onReachScore;

    public TMP_Text scoreText;
    public TMP_Text highscoreText;

    int score = 0;
    int highscore = 0;
    int highestY = 0;
    bool t1SoundPlayed = false;
    bool t2SoundPlayed = false;


    void Start()
    {
        scoreText.text = score.ToString();
        highscoreText.text = highscore.ToString();
    }

    public void CheckTowerHeight(int blockY)
    {
        if (blockY > highestY)
        {
            highestY = blockY;
            score = highestY;
            scoreText.text = score.ToString();
            sfxEvent.Invoke(scoreSound);
        }

        // Change tier of background objects, 0 = low, 1 = mid, 2 = high.
        if(score < 4)
        {
            onReachScore.Invoke(0);
        }
        else if(score >= 5 && score < 10)
        {
            onReachScore.Invoke(1);
            if (t1SoundPlayed == false)
            {
                print("T1 sound played");
                sfxEvent.Invoke(scoreMilestoneSound);
                t1SoundPlayed = true;
            }
        }
        else
        {
            onReachScore.Invoke(2);
            if (t2SoundPlayed == false)
            {
                sfxEvent.Invoke(scoreMilestoneSound);
                t2SoundPlayed = true;
            }
        }
    }
}
