using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{

    //How much jump force is added when picking the power-up
    public float addedForce;

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
            gameObject.SetActive(false);

            picked = true;

            powerUpController.ActivatePowerUp("jump", addedForce, powerupTime);
        }
    }
}
