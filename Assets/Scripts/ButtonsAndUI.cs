using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsAndUI : MonoBehaviour
{
    public void GoToMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
