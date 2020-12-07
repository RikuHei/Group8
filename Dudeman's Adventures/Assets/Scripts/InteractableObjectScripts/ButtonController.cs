using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public bool buttonIsActivated = false; 
    private bool characterInRange = false;
    public Animator animator;

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
    }

    public void ButtonPress()
    {
        animator = GetComponent<Animator>();
        if (buttonIsActivated == false)
        {
            buttonIsActivated = true;
            animator.SetBool("IsButtonPressed", true);
        }

        else if (buttonIsActivated == true)
        {
            buttonIsActivated = false;
            animator.SetBool("IsButtonPressed", false);
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
