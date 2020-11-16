using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public int damage = 40;
    public int explosionTimer = 50;
    public Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = transform.right * bulletSpeed;    
    }

    //With this we can make sure that there arent any bullets floating forever
    void Update()
    {
        Destroy(gameObject, explosionTimer);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Collision detector for destroyable blocks
        /*Destroyable destroyable = hitInfo.GetComponent<Destroyable>();
        if(destroyable != null)
        {
            destroyable.TakeDamage(damage);
        }

        //Collision detector for enemies
        /*Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }*/
        Destroy(gameObject);
    }
}
