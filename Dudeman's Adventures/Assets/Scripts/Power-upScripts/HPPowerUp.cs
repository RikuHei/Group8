using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPowerUp : MonoBehaviour
{
    //Amount of HP gained when picking the power-up
    public int HP_amount;

    //Boolean to create power-ups that give maximum HP
    public bool fullHP;

    private bool picked = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player"  && !picked)
        {
            //Getting the script that handles the HP
            GameObject player = collider.gameObject;
            RestartOnPlayerDeath playerScript = player.GetComponent<RestartOnPlayerDeath>();

            picked = true;

            if(fullHP == true)
            {
                playerScript.currentHealth = playerScript.maxHealth;
            }
            else
            {
                //Checking if the health goes over max health when the power-up is picked up
                if(playerScript.maxHealth < (playerScript.currentHealth + HP_amount))
                {
                    Debug.Log("OVER LIMIT");
                    playerScript.currentHealth = playerScript.maxHealth;
                }
                else
                {
                    Debug.Log("UNDER");
                    playerScript.currentHealth += HP_amount;
                }
            }

            playerScript.healthBar.SetHealth(playerScript.currentHealth);

            Destroy(gameObject);

            Debug.Log(playerScript.maxHealth);
            Debug.Log(playerScript.currentHealth);

        }
    }
}
