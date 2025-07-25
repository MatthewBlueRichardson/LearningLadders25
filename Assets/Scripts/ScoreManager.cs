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

    [Header("Events")]
    [SerializeField] private IntEvent onReachScore;

    [Header("Game Objects")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private TMP_Text gameOverHighScoreText; // TODO: Revisit score manager and refactor it!
    [SerializeField] private TMP_Text gameOverScoreText;

    private int score = 0;
    private int highscore = 0;
    private int highestY = 0;
    private bool t1SoundPlayed = false;
    private bool t2SoundPlayed = false;

    void Start()
    {
        scoreText.text = score.ToString();
        highscoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        gameOverHighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }

    public void CheckTowerHeight(int blockY)
    {
        if (blockY > highestY)
        {
            highestY = blockY;
            score = highestY;
            scoreText.text = score.ToString();
            gameOverScoreText.text = "Score: " +  score.ToString();
            sfxEvent.Invoke(scoreSound);
            PlayerPrefs.SetInt("Score", score);
            if (score > PlayerPrefs.GetInt("HighScore"))
            {
                Debug.Log("New high score: " + score);
                PlayerPrefs.SetInt("HighScore", score);
                gameOverHighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
            }
                
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
