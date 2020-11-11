using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreakByPlayerStep : MonoBehaviour
{

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            Invoke("BreakPlatform", 0.1f);
            Destroy(gameObject, 0.1f);
        }
    }

    void BreakPlatform()
    {
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
