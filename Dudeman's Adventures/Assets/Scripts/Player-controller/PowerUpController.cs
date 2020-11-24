using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private bool jumpBoost;
    private float normalForce;
    private float jumpBoostForce;
    private float jumpBoostTime;

    private bool damageImmunity;
    private float damageImmunityTime;

    private CharacterController2D playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<CharacterController2D>();
        normalForce = playerController.m_JumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        if(jumpBoost)
        {
            jumpBoostTime -= Time.deltaTime;


            if(jumpBoostTime <= 0)
            {
                playerController.m_JumpForce = normalForce;
                jumpBoost = false;
            }
        }
    }

    public void ActivatePowerUp(string name, float force, float time)
    {
        if(name == "jump")
        {
            jumpBoost = true;
            jumpBoostForce = force;
            jumpBoostTime = time;
            
            playerController.m_JumpForce += jumpBoostForce;
        }
        if(name == "immunity")
        {
            damageImmunity = true;
            damageImmunityTime = time;
        }
    }
}
