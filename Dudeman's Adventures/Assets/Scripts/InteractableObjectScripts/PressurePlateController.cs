using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{

    public bool pressurePlateIsActivated = false; 

    //When something hits the hitbox of the pressure plate, change the boolean to true
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Moveable") 
        {
            pressurePlateIsActivated = true;
        }
    }

    //Vice versa
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Moveable") 
        {
            pressurePlateIsActivated = false;
        }
    }
}
