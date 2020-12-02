using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text textBox;

    private string levelName;
    private string highScoreName;

    private float scoreTime = 0;

    //private int intTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = scoreTime.ToString();
        
        levelName = SceneManager.GetActiveScene().name;
        highScoreName = levelName + "HighScore";

        if(PlayerPrefs.HasKey(highScoreName) == false)
        {
            PlayerPrefs.SetFloat(highScoreName, 0);
        }

        Debug.Log(PlayerPrefs.GetFloat(highScoreName));
    }

    // Update is called once per frame
    void Update()
    {
        scoreTime += Time.deltaTime;
        textBox.text = scoreTime.ToString("0");
    }
}
