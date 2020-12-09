using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletController : MonoBehaviour
{
 

    void Start()
    {
        
    }

    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
            GameObject.Find("Player").GetComponent<RestartOnPlayerDeath>().TakeDamage(5);
            Destroy(this.gameObject);
    }
}