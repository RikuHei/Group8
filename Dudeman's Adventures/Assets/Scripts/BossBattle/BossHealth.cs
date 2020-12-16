using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    public int health = 1550;

    public bool isInvulnerable = false;

    private GameObject nextLevel;

    private bool bossDead = false;

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
        {
            return;
        }

        if (health <= 750)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // play death animation.
        GetComponent<Animator>().SetBool("IsDead", true);
        bossDead = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        nextLevel = GameObject.Find("NextLevel");
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossDead)
        {
            Debug.Log("door should not be active now");
            nextLevel.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            Debug.Log("door should be active now");
            nextLevel.GetComponent<Collider2D>().enabled = true;
        }
    }
}
