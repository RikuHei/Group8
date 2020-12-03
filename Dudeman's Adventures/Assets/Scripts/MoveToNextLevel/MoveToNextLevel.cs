using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{

    public int nextSceneLoad;

    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;

        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            //Save the score if its high score
            if(PlayerPrefs.HasKey(scoreManager.highScoreName) == false)
            {
                PlayerPrefs.SetFloat(scoreManager.highScoreName, scoreManager.scoreTime);
                Debug.Log("There wasn't a high score, new high score: " + scoreManager.scoreTime);
            }
            else if(PlayerPrefs.GetFloat(scoreManager.highScoreName) > scoreManager.scoreTime)
            {
                PlayerPrefs.SetFloat(scoreManager.highScoreName, scoreManager.scoreTime);
                Debug.Log("Replacing old high score with " + scoreManager.highScoreName + ", " + scoreManager.scoreTime);
            }
            
            if (SceneManager.GetActiveScene().buildIndex == 7)
            {
                Debug.Log("You Completed ALL Levels");
                Debug.Log("---------------------");
                Debug.Log("HIGH SCORES:");
                Debug.Log("Level 1 = " + PlayerPrefs.GetFloat("Level01HighScore"));
                Debug.Log("Level 2 = " + PlayerPrefs.GetFloat("Level02HighScore"));
                Debug.Log("Level 3 = " + PlayerPrefs.GetFloat("Level03HighScore"));
                Debug.Log("Level 4 = " + PlayerPrefs.GetFloat("Level04HighScore"));
                Debug.Log("Level 5 = " + PlayerPrefs.GetFloat("Level05HighScore"));
                Debug.Log("Level 6 = " + PlayerPrefs.GetFloat("Level06HighScore"));

                //Show Win Screen or Somethin.
            }
            else
            {
                //Move to next level
                SceneManager.LoadScene(nextSceneLoad);

                //Reload the score timer
                scoreManager.ReloadScoreTimer();

                //Setting Int for Index
                if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                }
            }
        }
    }

}
