using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public bool instaDeath;
    public bool enemyInstaDeath = true;
    public int spikeDamage = 10;

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.name == "Player" && instaDeath)
        {
            GameObject.Find("Player").GetComponent<RestartOnPlayerDeath>().Die();
        }
        else if(collision.transform.name == "Player" && !instaDeath)
        {
            GameObject.Find("Player").GetComponent<RestartOnPlayerDeath>().TakeDamage(spikeDamage);
        }
        else if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && enemyInstaDeath)
        {
            Destroy(collision.collider.gameObject);
        }
    }
}
