using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    private float timeBtwShots;
    public float startTimeBtwShots;

    public Transform player;
    public GameObject AIBulletController;
    public Transform ShootPoint;
    public Transform Muzzle;
    [SerializeField]
    float aggroRange;
    [SerializeField] // checkbox for the dev when placing an AI if it is facing Left or not !!! VERY IMPORTANT !!!
    bool isFacingLeft = true;

    Vector3 baseScale;

    Rigidbody2D rb2d;

    private bool isAggro = false;
    private bool isSearching = false;

    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player").transform;
         timeBtwShots = startTimeBtwShots;
         baseScale = transform.localScale;
         rb2d = GetComponent<Rigidbody2D>();
         rb2d.gravityScale = 10f; 
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
                    //Player will be chased for 2 seconds and after that the AI stops. 
                    //Might wanna do a coroutine for this in the future.
                    Invoke("StopChasingPlayer", 2);
                }
            }
        }

        if (isAggro == true)
        {
            ChasePlayer();
            if(timeBtwShots <= 0)
            {
                Shoot();
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
            
        }
    }

    bool DetectPlayer(float distance)
    {
        bool val = false;
        float castDist = distance;
        int maskPlayer = 1 << LayerMask.NameToLayer("Player");
        int maskTerrain = 1 << LayerMask.NameToLayer("Terrain");
        int combinedMask = maskPlayer | maskTerrain;

        if(isFacingLeft)
        {
            castDist = -distance;
        }

        Vector2 endPos = ShootPoint.position + Vector3.right * castDist;

        RaycastHit2D hit = Physics2D.Linecast(ShootPoint.position, endPos, combinedMask);

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

            Debug.DrawLine(ShootPoint.position, endPos, Color.blue);
        }
        
        else
        {
            Debug.DrawLine(ShootPoint.position, endPos, Color.red);
        }

        return val;

    }

    void Shoot()
    {
        Instantiate(AIBulletController, Muzzle.position, Quaternion.identity);
        timeBtwShots = startTimeBtwShots;
    }

    void ChasePlayer()
    {
        Vector3 newScale = baseScale;
        
        if(transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            newScale.x = -baseScale.x;
            isFacingLeft = false;
            transform.localScale = newScale;
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            newScale.x = baseScale.x;
            isFacingLeft = true;
            transform.localScale = newScale;
        }
        
    }

        void StopChasingPlayer()
    {
        isAggro = false;
        isSearching = false;
        rb2d.velocity = new Vector2(0, 0);
    }
    
}