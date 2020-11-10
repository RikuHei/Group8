using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    //Fields for choosing interactables that are used to open the door (drag'n'drop)
    [SerializeField] GameObject[] pressurePlates;
    [SerializeField] GameObject[] buttons;

    int notActivatedPlates = 0;
    int notActivatedButtons = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetNOPlates();
        GetNOButtons();
    }

    public int GetNOPlates()
    {
        int x = 0;

        for (int i = 0; i < pressurePlates.Length; i++)
        {
            if(pressurePlates[i].GetComponent<PressurePlateController>().pressurePlateIsActivated == false)
            {
                x++;
            }
            else if(pressurePlates[i].GetComponent<PressurePlateController>().pressurePlateIsActivated == true)
            {
                notActivatedPlates--;
            }
        }

        notActivatedPlates = x;

        return notActivatedPlates;
    }

    public int GetNOButtons()
    {
        int x = 0;

        for (int i = 0; i < buttons.Length; i++)
        {
            if(buttons[i].GetComponent<ButtonController>().buttonIsActivated == false)
            {
                x++;
            }
            else if(buttons[i].GetComponent<ButtonController>().buttonIsActivated == true)
            {
                notActivatedButtons--;
            }
        }

        notActivatedButtons = x;

        return notActivatedButtons;
    }

    //This should be animated I guess, now it just disables the collider & renderer of the door
    public void OpenDoor()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    //We can use this to make the door appear again if for example one of the pressure plates is unactivated
    /*
    public void CloseDoor()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }
    */

    // Update is called once per frame
    void Update()
    {
        GetNOPlates();
        GetNOButtons();

        if(notActivatedPlates <= 0 && notActivatedButtons <= 0)
        {
            OpenDoor();
        }
    }
}
