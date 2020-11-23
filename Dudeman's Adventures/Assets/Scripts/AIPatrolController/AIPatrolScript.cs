using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolScript : MonoBehaviour
{
    const string LEFT = "left";
    const string RIGHT = "right";


    [SerializeField]
    Transform castPos;

    [SerializeField]
    float baseCastDist;

    string facingDirection;

    Vector3 baseScale;

    Rigidbody2D rb2d;
    float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        facingDirection = RIGHT;

        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);

        if(IsHittingWall())
        {
            print("hit wall");
        }
    }

    bool IsHittingWall()
    {
        bool val = false;

        float castDist = baseCastDist;

        if(facingDirection == LEFT)
        {
            castDist = -baseCastDist;
        }
        else
        {
            castDist = baseCastDist;
        }
        
        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        if(Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Terrain")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }
}
