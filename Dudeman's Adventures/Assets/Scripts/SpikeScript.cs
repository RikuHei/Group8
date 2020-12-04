using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public bool instaDeath;
    public bool enemyInstaDeath = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.name == "Player" && instaDeath)
        {
            GameObject.Find("Player").GetComponent<RestartOnPlayerDeath>().Die();
        }
        else if(collision.transform.name == "Player" && !instaDeath)
        {
            GameObject.Find("Player").GetComponent<RestartOnPlayerDeath>().TakeDamage(5);
        }
        else if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && enemyInstaDeath)
        {
            Destroy(collision.collider.gameObject);
        }
    }
}
