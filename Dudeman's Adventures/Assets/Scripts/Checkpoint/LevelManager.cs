using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject currentCheckpoint;

    private CharacterController2D player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RespawnPlayer()
    {
        Debug.Log("Player respawned");
        player.transform.position = currentCheckpoint.transform.position;
    }

}
