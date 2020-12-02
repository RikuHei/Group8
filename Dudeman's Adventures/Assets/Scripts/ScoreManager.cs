using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text textBox;

    private string levelName;
    public string highScoreName;

    public float scoreTime = 0;

    private float savedScore;
    private string savedScoreName;

    // Start is called before the first frame update
    void Start()
    {
        levelName = SceneManager.GetActiveScene().name;
        savedScoreName = levelName + "SavedScore";
        highScoreName = levelName + "HighScore";

        if(PlayerPrefs.HasKey(savedScoreName) == true)
        {
            savedScore = PlayerPrefs.GetFloat(savedScoreName);
            scoreTime = savedScore;
            PlayerPrefs.DeleteKey(savedScoreName);
        }
        textBox.text = scoreTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreTime += Time.deltaTime;
        textBox.text = scoreTime.ToString("0");
    }

    public void ReloadScoreTimer()
    {
        textBox.text = scoreTime.ToString();

        levelName = SceneManager.GetActiveScene().name;
        highScoreName = levelName + "HighScore";

        if(PlayerPrefs.HasKey(highScoreName) == true)
        {
            Debug.Log(PlayerPrefs.GetFloat(highScoreName));
        }
    }

    public void SaveScoreOnDeath()
    {
        PlayerPrefs.SetFloat(savedScoreName, scoreTime);
        Debug.Log("Player died, saving the time:");
        Debug.Log(PlayerPrefs.GetFloat(savedScoreName));
    }
}
