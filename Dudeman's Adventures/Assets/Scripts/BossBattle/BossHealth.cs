using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    public int health = 1000;

    public bool isInvulnerable = false;

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
        {
            return;
        }

        if (health <= 500)
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
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
