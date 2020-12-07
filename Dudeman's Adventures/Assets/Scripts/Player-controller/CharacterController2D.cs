using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] public float m_JumpForce = 70f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    public Animator animator;
    public Transform shootPoint;

    Vector3 localScale;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private int defaultPlayerGravity = 1;
    private int playerJumpGravity = 15;

    public AudioClip[] jump;
    public AudioSource audioSource;
    public AudioListener audioListener;
    private AudioClip jumpClip;


    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioListener = GetComponent<AudioListener>();
        audioSource = gameObject.GetComponent<AudioSource>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        animator.SetBool("IsJumping", true);
        m_Rigidbody2D.gravityScale = playerJumpGravity;

        CheckWeaponDirection();

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                animator.SetBool("IsJumping", false);
                m_Rigidbody2D.gravityScale = defaultPlayerGravity;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            animator.SetBool("IsJumping", false);
            m_Rigidbody2D.gravityScale = defaultPlayerGravity;

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                // move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            animator.SetBool("IsJumping", true);
            m_Rigidbody2D.gravityScale = playerJumpGravity;

            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse);
            
            PlayRandomJump();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1. 
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //Switch the shoot point of the gun according to the position of the player and key inputs
    void CheckWeaponDirection()
    {
        //Added a null check here to make sure that the script doesnt break scenes where there isnt a gun
        if (shootPoint != null)
        {
            //While holding 'w', the shoot point is directly upwards
            if (Input.GetKey(KeyCode.W))
            {
                shootPoint.rotation = Quaternion.Euler(0, 0, 90 * transform.localScale.y);
            }

            //while holding 's', the shoot point is directly downwards
            else if (Input.GetKey(KeyCode.S))
            {
                shootPoint.rotation = Quaternion.Euler(0, 0, 270 * transform.localScale.y);
            }
            else
            {
                //when the character is facing right, the shoot point is pointed right
                if (m_FacingRight)
                {
                    shootPoint.rotation = Quaternion.Euler(0, 0, 0);
                }
                //when the character is facing left, the shoot point is pretty obviously pointed to the left
                else if (!m_FacingRight)
                {
                    shootPoint.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }
    }

    //function to play random jump sound
    void PlayRandomJump()
    {
        int index = Random.Range(0, jump.Length);
        jumpClip = jump[index];
        audioSource.clip = jumpClip;
        audioSource.Play();
    }
}