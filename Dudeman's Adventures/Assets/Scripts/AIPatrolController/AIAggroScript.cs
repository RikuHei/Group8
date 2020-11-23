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
    public float aggroSpeed;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //Check distance to the player
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if(distToPlayer < aggroRange)
        {
            //Aggros the player
            ChasePlayer();
            Debug.Log("aggro");
        }
        else
        {
            //Stop aggro
            StopChasingPlayer();
        }
    }

    void ChasePlayer()
    {
        var AISpeed = GameObject.Find("NPC_patrol").GetComponent<AIPatrolScript>();
        AISpeed.moveSpeed = aggroSpeed;

        if(transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(aggroSpeed, 0);
        }
        else if (transform.position.x > player.position.x)
        {
            rb2d.velocity = new Vector2(-aggroSpeed, 0);
        }
    }

    void StopChasingPlayer()
    {

    }
}
