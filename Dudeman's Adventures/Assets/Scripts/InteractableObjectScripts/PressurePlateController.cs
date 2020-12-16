using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public bool pressurePlateIsActivated = false; 

    public bool onlyActivableByBoulder = false;
    public bool pressurePlateTimer = false;
    public float timeUntilReset;
    
    private bool timerRunning = false;
    private Coroutine timerRoutine;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Pressure plate is active when the object stays on it
    void OnCollisionStay2D(Collision2D other)
    {
        if(onlyActivableByBoulder)
        {
            if (other.transform.tag == "Moveable") 
            {
                pressurePlateIsActivated = true;
                spriteRenderer.color = Color.green;
            }
        }
        else
        {
            if (other.transform.tag == "Moveable" || other.transform.tag == "Player") 
            {
                pressurePlateIsActivated = true;
                spriteRenderer.color = Color.green;
            }
        }
    }

    //Vice versa
    void OnCollisionExit2D(Collision2D other)
    {
        if(onlyActivableByBoulder)
        {
            if (other.transform.tag == "Moveable") 
            {
                pressurePlateIsActivated = false;
                spriteRenderer.color = Color.red;
            }
        }
        else
        {
            if (other.transform.tag == "Moveable" || other.transform.tag == "Player" && !pressurePlateTimer) 
            {
                pressurePlateIsActivated = false;
                spriteRenderer.color = Color.red;
            }
            if (other.transform.tag == "Player" && pressurePlateTimer && !timerRunning) 
            {
                timerRoutine = StartCoroutine(ResetTimer(timeUntilReset));
            }
            if (other.transform.tag == "Player" && pressurePlateTimer && timerRunning) 
            {
                StopCoroutine(timerRoutine);
                timerRunning = false;
                timerRoutine = StartCoroutine(ResetTimer(timeUntilReset));
            }
        }

    }

    public IEnumerator ResetTimer(float time)
    {
        timerRunning = true;
        yield return new WaitForSeconds(time);
        timerRunning = false;
        pressurePlateIsActivated = false;
        spriteRenderer.color = Color.red;
    }
}
