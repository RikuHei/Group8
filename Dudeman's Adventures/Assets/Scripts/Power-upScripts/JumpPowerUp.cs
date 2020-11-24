using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    //How much jump force is added when picking the power-up
    public float addedForce;

    //How long the power-up lasts when picked up
    public float powerupTime;

    //Variables for changing the jump force value
    GameObject player;
    CharacterController2D playerScript;

    void Awake()
    {
        //Getting the script that handles the jump force
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<CharacterController2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            Destroy(gameObject);

            Debug.Log(playerScript.m_JumpForce);

            StartCoroutine(PowerUpWearOff(5f));
            //playerScript.m_JumpForce += addedForce;
            //StartCoroutine(powerUpTimer(powerupTime));
            //Debug.Log(playerScript.m_JumpForce);
        }
    }

    IEnumerator PowerUpWearOff(float time)
    {
        Debug.Log("TEST");
        playerScript.m_JumpForce += addedForce;
        yield return new WaitForSeconds(time);
        Debug.Log("TEST1212");
        playerScript.m_JumpForce -= addedForce;


        Debug.Log(playerScript.m_JumpForce);
    }

/*
    void CancelPowerUp()
    {
        playerScript.m_JumpForce -= addedForce;
        Debug.Log("Cancelled");
    }
*/


/*
    private IEnumerator powerUpTimer(float time)
    {
        Debug.Log("Timer starting");
        Debug.Log(playerScript.m_JumpForce);
        yield return new WaitForSeconds(time);

        playerScript.m_JumpForce -= addedForce;

        Debug.Log("Timer ending");
        Debug.Log(playerScript.m_JumpForce);
    }


*/
    

}
