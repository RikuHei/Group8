using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool reversedDoor = false;

    //Fields for choosing interactables that are used to open the door (drag'n'drop)
    [SerializeField] GameObject[] pressurePlates;
    [SerializeField] GameObject[] levers;

    int notActivatedPlates = 0;
    int notActivatedLevers = 0;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GetNOPlates();
        GetNOLevers();
        animator = GetComponent<Animator>();
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

    public int GetNOLevers()
    {
        int x = 0;

        for (int i = 0; i < levers.Length; i++)
        {
            if(levers[i].GetComponent<ButtonController>().buttonIsActivated == false)
            {
                x++;
            }
            else if(levers[i].GetComponent<ButtonController>().buttonIsActivated == true)
            {
                notActivatedLevers--;
            }
        }

        notActivatedLevers = x;

        return notActivatedLevers;
    }

    public void OpenDoor()
    {
        GetComponent<Collider2D>().enabled = false;
        animator.SetBool("OpenDoor", true);
    }

    public void CloseDoor()
    {
        GetComponent<Collider2D>().enabled = true;
        animator.SetBool("OpenDoor", false);
    }

    // Update is called once per frame
    void Update()
    {
        GetNOPlates();
        GetNOLevers();

        if(reversedDoor)
        {
            if(notActivatedPlates <= 0 && notActivatedLevers <= 0)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
        else
        {
            if(notActivatedPlates <= 0 && notActivatedLevers <= 0)
            {
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }
        }
    }
}
