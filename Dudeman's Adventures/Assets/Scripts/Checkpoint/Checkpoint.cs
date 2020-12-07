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
            // update spawnpoint position when player collides with checkpoint
            levelManager.spawnPoint.transform.position = gameObject.transform.position;
            // destroy checkpoint after spawnpoint has been updated
            Destroy(gameObject);

        }
    }
}
