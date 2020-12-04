using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreScript : MonoBehaviour
{
    public Text[] highScoreFields;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < highScoreFields.Length; i++)
        {
            if(PlayerPrefs.HasKey("Level0" + (i + 1) + "HighScore") == true)
            {
                float highScore = PlayerPrefs.GetFloat("Level0" + (i + 1) + "HighScore");
                if(highScore > 999.99)
                {
                    highScoreFields[i].fontSize = 20;
                }
                highScoreFields[i].text = highScore.ToString("0.00") + "s";
            }
        }
    }
}
