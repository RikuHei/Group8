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
    }

    // Update is called once per frame
    void Update()
    {
        //Pressing Space makes the player take 1 damage.
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(1);
        }
        if (RestartController.isDead)
        {
            healthBar.SetHealth(0);
            movementController.runSpeed = 0;
            characterController2D.m_JumpForce = 0;
            animator.SetBool("IsDead", true);
        }
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
        if (!damageImmunity && !immunityOnCooldown)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            immunityFromPowerUp = false;
            EnableDamageImmunity(defaultImmunityTime, immunityFromPowerUp);

            if (currentHealth < 1)
            {
                RestartController.isDead = true;
                Die();
            }
        }
    }

    public void Die()
    {
        Invoke("RestartSceneOnDeath", 1);
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
        yield return new WaitForSeconds(time);
        damageImmunity = false;
        immunityTimerIsRunning = false;
        cooldownRoutine = StartCoroutine(CooldownTimer(immunityCooldownTime));
    }

    public IEnumerator CooldownTimer(float time)
    {
        immunityOnCooldown = true;
        yield return new WaitForSeconds(time);
        immunityOnCooldown = false;
    }
}



