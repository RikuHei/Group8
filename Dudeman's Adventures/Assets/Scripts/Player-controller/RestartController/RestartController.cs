using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartController : MonoBehaviour
{

    Rigidbody2D rb;
    AudioSource audioSource;
    public LevelManager levelManager;
    public static bool isDead = false;
    private ScoreManager scoreManager;
    [SerializeField] private bool ResetScoreOnDeath = false;

    // if player falls below this point, the game will restart
    private float fallZone = -30f;

    public RestartOnPlayerDeath restartOnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        levelManager = FindObjectOfType<LevelManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        restartOnPlayerDeath = FindObjectOfType<RestartOnPlayerDeath>();
    }

    public void RestartScene()
    {
        if (!ResetScoreOnDeath)
        {
            scoreManager.SaveScoreOnDeath();
        }
        isDead = false;
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
        Debug.Log("Game restarted");
    }

    // Hard restart scene with key "R"
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
            restartOnPlayerDeath.Die();
        }

    }

    void FixedUpdate()
    {

    }
}
