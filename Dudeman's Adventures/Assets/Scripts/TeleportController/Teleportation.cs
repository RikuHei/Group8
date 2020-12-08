using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{

    public GameObject TeleportToLocation, Player;
    public static int StartTeleport = 0;
    public AudioSource audioSource;
    public AudioListener audioListener;
    public AudioClip soundClip;

    void Start()
    {
        audioListener = GetComponent<AudioListener>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void OnTriggerEnter2D(Collider2D collision) 
    {
        //Checks if the object is tagged Player and if the player has passed key positions.
        if (collision.gameObject.name == "Player" && Teleportation.StartTeleport == 1)
        {
            Teleportation.StartTeleport = 0;
            Invoke("Teleport", 0.2f);
        }
    }

    void Teleport()
    {
        audioSource.clip = soundClip;
        audioSource.Play();
        //Teleports the player to the position of the set teleporter's vector position.
        Player.transform.position = new Vector3 (TeleportToLocation.transform.position.x, TeleportToLocation.transform.position.y, -10);
    }
}
