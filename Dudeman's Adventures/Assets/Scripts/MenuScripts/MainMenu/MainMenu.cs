using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HighScores()
    {
        SceneManager.LoadScene("HighScores");
    }

    public void HelpScene()
    {
        SceneManager.LoadScene("HelpScene");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT was pressed");
        Application.Quit();
    }

}
