﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableScript : MonoBehaviour
{
    public int health = 100;
    public float itemSpawnChance;
    public GameObject spawnableItem;
    public Animator animator;

    void Start()
    {        
        if(animator != null)
        {
            //Setting the health as an integer in the animator
            animator.SetInteger("HP", health);
        }
    }

    public void TakeDamage (int damage)
    {
        health -= damage;

        if(animator != null)
        {
            //Setting the HP integer accordingly
            animator.SetInteger("HP", health);
        }

        if(health <= 0)
        {
            Die();
        }
    }

    public void SpawnHpItem()
    {
        if(Random.value <= itemSpawnChance)
        {
            Instantiate(spawnableItem, transform.position, transform.rotation);
        }
    }

    void Die()
    {
        if(spawnableItem != null)
        {
            SpawnHpItem();
            Destroy(gameObject);
        }
        else if(animator != null)
        {
            //Destroying the gameobject after a short pause to play the animation, gotta find a better way
            Destroy(gameObject, 1f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
