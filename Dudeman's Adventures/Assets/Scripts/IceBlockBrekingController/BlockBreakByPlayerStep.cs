using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreakByPlayerStep : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            animator.SetBool("playerCollided", true);
            Destroy(gameObject, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
