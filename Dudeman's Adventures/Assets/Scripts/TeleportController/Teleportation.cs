using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{

    public GameObject Teleporter, Player;
    public static int StartTeleport = 0;


    public void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player" && Teleportation.StartTeleport == 1)
        {
            Teleportation.StartTeleport = 0;
            StartCoroutine (Teleport());
        }
    }

    IEnumerator Teleport()
    {
        //Waits for nothing atm but this can be later on changed.
        yield return new WaitForSeconds(0);
        //Teleports the player to the position of the set teleporter's vector position.
        Player.transform.position = new Vector2 (Teleporter.transform.position.x, Teleporter.transform.position.y);
    }
}
