using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public bool pressurePlateIsActivated = false; 

    public bool pressurePlateTimer = false;
    public float timeUntilReset;
    

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Pressure plate is active when the object stays on it
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.tag == "Moveable" || other.transform.tag == "Player") 
        {
            pressurePlateIsActivated = true;
            spriteRenderer.color = Color.green;
        }
    }

    //Vice versa
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Moveable" || other.transform.tag == "Player" && !pressurePlateTimer) 
        {
            pressurePlateIsActivated = false;
            spriteRenderer.color = Color.red;
        }
        if (other.transform.tag == "Player" && pressurePlateTimer) 
        {
            StartCoroutine(ResetTimer(timeUntilReset));
        }
    }

    public IEnumerator ResetTimer(float time)
    {
        yield return new WaitForSeconds(time);
        pressurePlateIsActivated = false;
        spriteRenderer.color = Color.red;
    }
}
