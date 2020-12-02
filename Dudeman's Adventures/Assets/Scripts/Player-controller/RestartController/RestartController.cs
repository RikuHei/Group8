using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartController : MonoBehaviour
{

    Rigidbody2D rb;
    AudioSource audioSource;
    public LevelManager levelManager;

    // if player falls below this point, the game will restart
    private float fallZone = -30f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
        Debug.Log("Game restarted");
    }

    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // track player position whitout resetting the y axis value
        // if player fall below fallZone value (-30) game will restart
        if (rb.transform.position.y < fallZone)
        {
            levelManager.RespawnPlayer();
        }

    }

    void FixedUpdate()
    {

    }
}
