using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public bool pressurePlateIsActivated = false; 

    // Update is called once per frame
    void Update()
    {

        //These can be used for animating the pressure plate

        if (pressurePlateIsActivated == true) 
        {
            //GetComponent<Animation>().Play ("pressurePlateDown");
        }

        if (pressurePlateIsActivated == false) 
        {
            //GetComponent<Animation>().Play ("pressurePlateUp");
        }
    }

    //When something hits the hitbox of the pressure plate, change the boolean to true
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Moveable") 
        {
            pressurePlateIsActivated = true;
            Debug.Log("PRESSED");
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
