using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public bool buttonIsActivated = false; 
    private bool characterInRange = false;

    // Update is called once per frame
    void Update()
    {
        if(characterInRange == true)
        {
            if(Input.GetButtonDown("Interact"))
            {
                ButtonPress();
            }
        }
        Debug.Log(characterInRange);
    }

    public void ButtonPress()
    {
        if (buttonIsActivated == false)
        {
            buttonIsActivated = true;
            Debug.Log("Button activated");
        }

        else if (buttonIsActivated == true)
        {
            buttonIsActivated = false;
            Debug.Log("Button disabled");
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Player") 
        {
            characterInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.tag == "Player") 
        {
            characterInRange = false;
        }
    }
}
