using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnPlayerDeath : MonoBehaviour
{

    public int maxHealth = 5;
    public int currentHealth;
    public float defaultImmunityTime;
    public bool damageImmunity;
    public bool timerIsRunning;
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
        if(!damageImmunity)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            EnableDamageImmunity(defaultImmunityTime);
            if (currentHealth < 1)
            {
                GameObject.Find("Player").GetComponent<MovementController>().runSpeed = 0;
                GameObject.Find("Player").GetComponent<CharacterController2D>().m_JumpForce = 0;
                animator.SetBool("IsDead", true);
                Invoke("RestartSceneOnDeath", 3f);
            }
        }
        if(damageImmunity)
        {
            Debug.Log("Immune");
        }
    }

    public void EnableDamageImmunity(float time)
    {
        if(!timerIsRunning)
        {
            damageImmunity = true;
            StartCoroutine(ImmunityTimer(time));
        }
    }

    public IEnumerator ImmunityTimer(float time)
    {
        timerIsRunning = true;
        yield return new WaitForSeconds(time);
        damageImmunity = false;
        timerIsRunning = false;
    }
}



