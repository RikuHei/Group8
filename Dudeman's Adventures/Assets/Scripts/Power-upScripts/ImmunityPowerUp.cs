using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityPowerUp : MonoBehaviour
{
    //How long the power-up lasts when picked up
    public float powerupTime;

    private PowerUpController powerUpController;

    private bool picked = false;

    void Start()
    {
        powerUpController = FindObjectOfType<PowerUpController>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.name == "Player" && !picked)
        {
            Destroy(gameObject);

            picked = true;

            powerUpController.ActivatePowerUp("immunity", 0, powerupTime);
        }
    }
}
