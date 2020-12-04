using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public LevelManager levelManager;

    void start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collided with checkpoint");
        if (col.name == "Player")
        {
            levelManager.currentCheckpoint = gameObject;
        }
    }
}
