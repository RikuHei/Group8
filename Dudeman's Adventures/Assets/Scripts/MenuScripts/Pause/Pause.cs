using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public static bool isPaused;
    public GameObject PauseMenu;

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape) && !RestartController.isDead){
            if(isPaused){
                ResumeGame();
            }else{
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void LevelSelection()
    {
        Time.timeScale = 1f;
        ResumeGame();
        SceneManager.LoadScene("LevelSelection");
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        ResumeGame();
        SceneManager.LoadScene("Menu");
    }
}
