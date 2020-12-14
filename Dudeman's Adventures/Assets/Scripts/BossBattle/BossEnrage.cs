using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossEnrage : StateMachineBehaviour
{
    // rb variable & Boss script variable
    Rigidbody2D rb;
    Boss boss;

    // boss gameObj & transform
    private GameObject bossGameObj;
    public Transform bossTrasnform;

    // bool to know wich way boss is moving
    private bool moveRight = false;

    // wall detection to know when the boss should switch direction
    private GameObject wallCheck;
    public float wallCheckRadius;
    private Transform wallTransform;

    // layermask for wall is Terrain
    public LayerMask whatIsWall;

    // bool to check if boss hit a wall
    private bool hittingWall;

    // speed for the boss
    public float speed = 20f;

    // castpoint Obj & transform castpoint
    GameObject castPointObj;
    Transform castPoint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // get boss Rigidbody2D
        rb = animator.GetComponent<Rigidbody2D>();

        // get boss script
        boss = animator.GetComponent<Boss>();

        // get boss obj and then save it's transform to bossTransform
        bossGameObj = GameObject.Find("Boss");
        bossTrasnform = bossGameObj.transform;

        // get ground detector for boss
        wallCheck = GameObject.Find("WallDetection");
        wallTransform = wallCheck.transform;

        // set castpoint obj into transform
        castPointObj = GameObject.Find("Castpoint");
        castPoint = castPointObj.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        // hitting wall bool to check if boss hit the wall and should turn
        hittingWall = Physics2D.OverlapCircle(wallTransform.position, wallCheckRadius, whatIsWall);

        // cast point distance
        float castDist = 12f;

        // castDist direction
        if (!moveRight)
        {
            castDist = -castDist;
        }

        // borrowed some code from AI aggro to make the boss gain movement speed
        // when player is in x range from him
        int maskPlayer = 1 << LayerMask.NameToLayer("Player");
        int maskTerrain = 1 << LayerMask.NameToLayer("Terrain");
        int combinedMask = maskPlayer | maskTerrain;

        Vector2 endPos = castPoint.position + Vector3.right * castDist;
        Vector2 endPos2 = castPoint.position + Vector3.left * castDist;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, combinedMask);
        RaycastHit2D hit2 = Physics2D.Linecast(castPoint.position, endPos2, combinedMask);

        // set some movement speed if boss sees player while enraged
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                speed = 46f;
            }
            else
            {
                speed = 16f;
            }
        }

        // check if boss hits wall and set moveRight bool accordingly
        if (hittingWall)
        {
            moveRight = !moveRight;
        }

        // if moving right set speed else set -speed
        if (moveRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

        // set boss direction based on wich way he is moving
        // Tried to use Rotate() but, for somereason it just made the boss into a helicopter
        if (rb.velocity.x > 0)
        {
            bossTrasnform.localScale = new Vector3(-1.3f, 1.3f, 1f);
        }
        else if (rb.velocity.x < 0)
        {
            bossTrasnform.localScale = new Vector3(1.3f, 1.3f, 1f);
        }

    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
