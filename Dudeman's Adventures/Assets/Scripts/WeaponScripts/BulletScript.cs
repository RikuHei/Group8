using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public int bulletDamage = 50;
    public int explosionTimer = 2;
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag != "Power-up")
        {
            //Destroy the bullet when hitting a collider
            Destroy(gameObject);

            //Collision detector for anything thats destroyable (tiles, enemies etc.)
            DestroyableScript destroyable = collider.GetComponent<DestroyableScript>();
            if(destroyable != null)
            {
                destroyable.TakeDamage(bulletDamage);
            }
        }
    }
}
