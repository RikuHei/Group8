using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAggroScript : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float aggroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        
        if(distToPlayer < aggroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
    }

    void ChasePlayer()
    {
        if(transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            //these values downhere need to be changed based on the size of the sprite.
            transform.localScale = new Vector2(0.5f, 0.8f);
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            //these values downhere need to be changed based on the size of the sprite.
            transform.localScale = new Vector2(-0.5f, 0.8f);
        }
    }

    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.name == "Player")
        {
             GameObject.Find("Player").GetComponent<RestartOnPlayerDeath>().TakeDamage(1);
        }

    }

}
