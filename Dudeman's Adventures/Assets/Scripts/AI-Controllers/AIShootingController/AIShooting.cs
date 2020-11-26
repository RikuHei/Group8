using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    public float speed;
    
    private float timeBtwShots;
    public float startTimeBtwShots;

    public Transform player;
    public GameObject AIBulletController;
    public Transform ShootPoint;
    [SerializeField]
    float aggroRange;
    [SerializeField]
    bool isFacingLeft = true;

    private bool isAggro = false;
    private bool isSearching = false;

    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player").transform;
         timeBtwShots = startTimeBtwShots;
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

        if(isFacingLeft)
        {
            castDist = -distance;
        }

        Vector2 endPos = ShootPoint.position + Vector3.right * castDist;

        RaycastHit2D hit = Physics2D.Linecast(ShootPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));

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
        Instantiate(AIBulletController, transform.position, Quaternion.identity);
        timeBtwShots = startTimeBtwShots;
    }

        void StopChasingPlayer()
    {
        isAggro = false;
        isSearching = false;
    }
    
}