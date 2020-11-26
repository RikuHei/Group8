using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private bool jumpBoostEnabled;
    private float normalForce;
    private float jumpBoostForce;
    private float jumpBoostTime;

    private bool damageImmunityCheck;
    private float immunityTime;

    private CharacterController2D characterController;
    private RestartOnPlayerDeath damageController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = FindObjectOfType<CharacterController2D>();
        damageController = FindObjectOfType<RestartOnPlayerDeath>();
        normalForce = characterController.m_JumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        if(jumpBoostEnabled)
        {
            jumpBoostTime -= Time.deltaTime;

            if(jumpBoostTime <= 0)
            {
                characterController.m_JumpForce = normalForce;
                jumpBoostEnabled = false;
            }
        }
        if(damageImmunityCheck)
        {
            damageController.EnableDamageImmunity(immunityTime, damageImmunityCheck);
            damageImmunityCheck = false;
        }
    }

    public void ActivatePowerUp(string name, float force, float time)
    {
        if(name == "jump")
        {
            if(jumpBoostEnabled)
            {
                characterController.m_JumpForce = normalForce;

                jumpBoostEnabled = true;
                jumpBoostForce = force;
                jumpBoostTime = time;
                
                characterController.m_JumpForce += jumpBoostForce;
                
            }
            else
            {
                jumpBoostEnabled = true;
                jumpBoostForce = force;
                jumpBoostTime = time;
                
                characterController.m_JumpForce += jumpBoostForce;
            }

        }
        if(name == "immunity")
        {
            damageImmunityCheck = true;
            immunityTime = time;
        }
    }
}
