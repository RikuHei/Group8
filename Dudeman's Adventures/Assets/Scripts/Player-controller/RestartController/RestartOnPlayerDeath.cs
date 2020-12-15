using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnPlayerDeath : MonoBehaviour
{

    public int maxHealth = 5;
    public int currentHealth;

    public float defaultImmunityTime;
    public float immunityCooldownTime;
    public bool damageImmunity;
    private bool immunityTimerIsRunning;
    private bool immunityFromPowerUp;
    private bool immunityOnCooldown;
    private Coroutine immunityRoutine;
    private Coroutine cooldownRoutine;

    public HealthBar healthBar;
    public Animator animator;

    private ScoreManager scoreManager;
    [SerializeField] private bool ResetScoreOnDeath = false;

    public LevelManager levelManager;
    public MovementController movementController;
    public CharacterController2D characterController2D;

    public AudioClip[] audioAudio;
    private AudioClip audioClip;
    public AudioSource audioSource;
    public AudioClip[] hitAudio;
    private AudioClip hitClip;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        scoreManager = FindObjectOfType<ScoreManager>();
        levelManager = FindObjectOfType<LevelManager>();
        movementController = FindObjectOfType<MovementController>();
        characterController2D = FindObjectOfType<CharacterController2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pressing Space makes the player take 1 damage.
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayRandomAudio();
        }
        if (RestartController.isDead)
        {
            healthBar.SetHealth(0);
            movementController.runSpeed = 0;
            characterController2D.m_JumpForce = 0;
            animator.SetBool("IsDead", true);
        }
        /*if (!immunityOnCooldown)
        {
            damageImmunity = false;
        }*/
    }

    public void RestartSceneOnDeath()
    {
        // set isDead state
        RestartController.isDead = false;

        // restore movement speed, jumpforce and return to idle animation
        movementController.runSpeed = 50;
        characterController2D.m_JumpForce = 70;
        animator.SetBool("IsDead", false);

        // set player hp back to max
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        // respawn player back to spawnpoint
        levelManager.RespawnPlayer();

        if (!ResetScoreOnDeath)
        {
            scoreManager.SaveScoreOnDeath();
        }
    }

    public void TakeDamage(int damage)
    {
        if (SceneManager.GetActiveScene().buildIndex != 6)
        {
            if (!damageImmunity)
            {
                currentHealth -= damage;
                healthBar.SetHealth(currentHealth);

                if (currentHealth < 1)
                {
                    RestartController.isDead = true;
                    Die();
                    PlayRandomHit();
                }
            }
            if (!immunityOnCooldown)
            {
                immunityFromPowerUp = false;
                EnableDamageImmunity(defaultImmunityTime, immunityFromPowerUp);
            }
        }
        else
        {
            if (!damageImmunity)
            {
                currentHealth -= damage;
                healthBar.SetHealth(currentHealth);

                if (currentHealth < 1)
                {
                    RestartController.isDead = true;
                    Invoke("DieInBossScene", 1);
                    PlayRandomHit();
                }
            }
            if (!immunityOnCooldown)
            {
                immunityFromPowerUp = false;
                EnableDamageImmunity(defaultImmunityTime, immunityFromPowerUp);
            }
        }
    }

    public void Die()
    {
        Invoke("RestartSceneOnDeath", 1);
    }

    void DieInBossScene()
    {
        // set isDead state
        RestartController.isDead = false;

        // restore movement speed, jumpforce and return to idle animation
        movementController.runSpeed = 50;
        characterController2D.m_JumpForce = 70;
        animator.SetBool("IsDead", false);

        // set player hp back to max
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        if (!ResetScoreOnDeath)
        {
            scoreManager.SaveScoreOnDeath();
        }
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
        Debug.Log("Game restarted");
    }

    public void EnableDamageImmunity(float time, bool powerUp)
    {
        if (!immunityTimerIsRunning)
        {
            damageImmunity = true;
            immunityRoutine = StartCoroutine(ImmunityTimer(time));
        }
        else if (immunityTimerIsRunning && powerUp)
        {
            StopCoroutine(immunityRoutine);
            damageImmunity = false;
            immunityTimerIsRunning = false;
            damageImmunity = true;
            immunityRoutine = StartCoroutine(ImmunityTimer(time));
        }
        else if (immunityOnCooldown && powerUp)
        {
            StopCoroutine(cooldownRoutine);
            damageImmunity = true;
            immunityRoutine = StartCoroutine(ImmunityTimer(time));
        }
    }

    public IEnumerator ImmunityTimer(float time)
    {
        immunityTimerIsRunning = true;
        Debug.Log("test1");
        yield return new WaitForSeconds(time);
        Debug.Log("test2");
        damageImmunity = false;
        immunityTimerIsRunning = false;
        cooldownRoutine = StartCoroutine(CooldownTimer(immunityCooldownTime));
    }

    public IEnumerator CooldownTimer(float time)
    {
        immunityOnCooldown = true;
        Debug.Log("test3");
        yield return new WaitForSeconds(time);
        Debug.Log("test4");
        immunityOnCooldown = false;
    }

    void PlayRandomAudio()
    {
        int index = Random.Range(0, audioAudio.Length);
        audioClip = audioAudio[index];
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    void PlayRandomHit()
    {
        int index = Random.Range(0, hitAudio.Length);
        hitClip = hitAudio[index];
        audioSource.clip = hitClip;
        audioSource.Play();
    }
}



