using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public bool instaDeath;
    public bool enemyInstaDeath = true;
    public int spikeDamage = 10;
    private int instaDeathDamage = 9000;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!RestartController.isDead)
        {
            if (collision.transform.name == "Player" && instaDeath)
            {
                GameObject.Find("Player").GetComponent<RestartOnPlayerDeath>().TakeDamage(instaDeathDamage);
            }
            else if (collision.transform.name == "Player" && !instaDeath)
            {
                GameObject.Find("Player").GetComponent<RestartOnPlayerDeath>().TakeDamage(spikeDamage);
            }
        }
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && enemyInstaDeath)
        {
            Destroy(collision.collider.gameObject);
        }

    }
}
