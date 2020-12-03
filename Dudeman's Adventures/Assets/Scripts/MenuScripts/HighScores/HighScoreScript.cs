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
                highScoreFields[i].text = PlayerPrefs.GetFloat("Level0" + (i + 1) + "HighScore").ToString("0.00") + "s";
            }
        }
    }
}
