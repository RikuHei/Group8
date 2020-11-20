using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{

    public Button[] lvlButtons;

    public void SelectLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("level", 2);
        Debug.Log(levelAt);
        for (int i = 1; i < lvlButtons.Length; i++)
        {
            if (1 + 2 > levelAt)
                lvlButtons[i].interactable = false;
        }
    }

}
