using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    AudioSource audioSource;
    bool isMoving = false;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }


    void FixedUpdate()
    {
        // move player character here
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;

    }
}
