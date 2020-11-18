using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        Destroy(this.gameObject);
        if (collision.transform.name == "Player")
        {
             GameObject.Find("Player").GetComponent<RestartOnPlayerDeath>().TakeDamage(1);
        }
       
    }
}
