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

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = scoreTime.ToString();

        levelName = SceneManager.GetActiveScene().name;
        highScoreName = levelName + "HighScore";
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
}
