using UnityEngine;

public class HighScoreIndicator : MonoBehaviour
{

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, PlayerPrefs.GetInt("HighScore"), transform.position.z);
    }

    public void ChangePosition()
    {
        transform.position = new Vector3(transform.position.x, PlayerPrefs.GetInt("HighScore"), transform.position.z);
    }
}
