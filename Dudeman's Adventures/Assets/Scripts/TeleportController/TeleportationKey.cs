using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TeleportationKey's are used to activate the actual teleporter. When player passes
//the key point, teleportation is made active. Without this teleportation will be buggy.
public class TeleportationKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) 
    {
       if(collision.gameObject.tag=="Player")
       {
           Teleportation.StartTeleport = 1;
       } 
    }
}
