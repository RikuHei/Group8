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

    [SerializeField]
    Transform castPoint;

    Rigidbody2D rb2d;

    bool isFacingLeft;
    private bool isAggro = false;
    private bool isSearching = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(DetectPlayer(aggroRange))
        {
            isAggro = true;
        }
        else
        {
            if(isAggro == true)
            {
                if(isSearching == false)
                {
                    isSearching = true;
                    Invoke("StopChasingPlayer", 2);
                }
            }
        }

        if (isAggro == true)
        {
            ChasePlayer();
        }
        // This code below is for a 'Dumber AI' that will chase the player even if they are
        // behind a wall. I am just leaving this here because don't know yet how we want to use the aggro mechanic.
       /* float distToPlayer = Vector2.Distance(transform.position, player.position);
        transform.Translate(0, 0, 0);
        
        if(distToPlayer < aggroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }*/
    }

    bool DetectPlayer(float distance)
    {
        bool val = false;
        float castDist = distance;

        if(isFacingLeft)
        {
            castDist = -distance;
        }

        Vector2 endPos = castPoint.position + Vector3.right * castDist;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));

        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else 
            {
                val = false;
            }

        }

        return val;

    }

    void ChasePlayer()
    {
        if(transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            //these values downhere need to be changed based on the size of the sprite.
            transform.localScale = new Vector2(0.5f, 0.8f);
            isFacingLeft = false;
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            //these values downhere need to be changed based on the size of the sprite.
            transform.localScale = new Vector2(-0.5f, 0.8f);
            isFacingLeft = true;
        }
    }

    void StopChasingPlayer()
    {
        isAggro = false;
        isSearching = false;
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
