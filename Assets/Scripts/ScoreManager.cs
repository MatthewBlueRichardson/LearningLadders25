using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text highscoreText;

    int score = 0;
    int highscore = 0;
    int highestY = 0;


    void Start()
    {
        scoreText.text = score.ToString();
        highscoreText.text = highscore.ToString();
    }

    public void CheckTowerHeight(int blockY)
    {
        print("After event " + blockY);
        if (blockY > highestY)
        {
            highestY = blockY;
            score = highestY;
            scoreText.text = score.ToString();
        }
    }
}
