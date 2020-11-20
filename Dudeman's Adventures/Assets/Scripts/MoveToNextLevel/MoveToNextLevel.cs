using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{

    public int nextSceneLoad;

    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            // Move to next level
            SceneManager.LoadScene(nextSceneLoad);
            Debug.Log(PlayerPrefs.GetInt("levelAt", nextSceneLoad));
            // Setting Int for Index
            if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }
        }
    }

}
