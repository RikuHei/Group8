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

    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pressing Space makes the player take 1 damage.
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(1);
        }
    }

    public void RestartSceneOnDeath()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
        Debug.Log("Game restarted");
    }

    public void TakeDamage(int damage)
    {
        if(!damageImmunity && !immunityOnCooldown)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            immunityFromPowerUp = false;
            EnableDamageImmunity(defaultImmunityTime, immunityFromPowerUp);

            if (currentHealth < 1)
            {
                GameObject.Find("Player").GetComponent<MovementController>().runSpeed = 0;
                GameObject.Find("Player").GetComponent<CharacterController2D>().m_JumpForce = 0;
                animator.SetBool("IsDead", true);
                Invoke("RestartSceneOnDeath", 3f);
            }
        }
    }

    public void EnableDamageImmunity(float time, bool powerUp)
    {
        if(!immunityTimerIsRunning)
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



