using LearningLadders.Audio;
using LearningLadders.EventSystem;
using UnityEngine;
using UnityEngine.InputSystem; // We need to use this library to access our input actions!

/* This script adapts on to Bendux's 2D platformer movement script, using the new input system.
 * You can find the full tutorial here: https://www.youtube.com/watch?v=24-BkpFSZuI */
public class PlatformerMovement : MonoBehaviour
{
    // Local Variables
    public Rigidbody2D torsoRb;
    private float horizontalInput;
    private bool facingRight = true;
    private Vector2 currentVelocity;

    // Player Movement Variables
    [Header("[Movement Properties]")]

    [Header("Walking")]
    [Tooltip("When true, the character is able to move horizontally.")]
    public bool canMove = true;
    [Tooltip("The maximum speed for the character.")]
    [SerializeField] private float speed;
    [SerializeField] private bool doFlip = true;

    [Header("Jumping")]
    [Tooltip("When true, the character is able to perform jumps.")]
    public bool canJump = true;
    [Tooltip("When true, the character jumps higher when the jump button is held, and shorter when tapped quickly.")]
    public bool holdToJumpHigher = true;  
    [Tooltip("The maximum jump height for the character.")]
    [SerializeField] private float jumpHeight;
    [Tooltip("When true, this applies gravityForce while the character is falling.")]
    [SerializeField] private bool useGravityForce = true;
    [Tooltip("The speed at which the character falls.")]
    [SerializeField] private float gravityForce;
    [Tooltip("The amount of times the character can jump before becoming ungrounded.")]
    [SerializeField] private int maxJumps = 1;

    [SerializeField] private PlayerRespawn respawnScript;

    [Header("Audio")]
    [SerializeField] private AudioClipSOEvent sfxEvent;
    [SerializeField] private AudioClipSO jumpSound;

    private int jumpCount = 0;
    private bool grounded = true;

    // Ground Check Variables
    [Header("[Ground Check]")]
    [Tooltip("An empty transform child positioned at the bottom of the character.")]
    public Transform groundCheck;
    [Tooltip("This is the layer(s) type which the character can jump on.")]
    public LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // rb = GetComponent<Rigidbody2D>(); // Finds local RigidBody2D component in the character.
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            // Applies horizontal movement, with smooth acceleration and deceleration.
            float horizontalVel = horizontalInput * speed;
            float smoothVel = Mathf.SmoothDamp(torsoRb.linearVelocity.x, horizontalVel, ref currentVelocity.x, 0.1f);
            torsoRb.linearVelocity = new Vector2(smoothVel, torsoRb.linearVelocity.y);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            // This if-statement flips the character's x-direction, to face in the direction of horizontal movement.
            if (!facingRight && horizontalInput > 0) // Facing left, but moving right?
            {
                Flip(); // Flip right!
            }
            else if (facingRight && horizontalInput < 0) // Facing right, but moving left?
            {
                Flip(); // Flip left!
            }
        }

        // This if-statement checks if the character is falling, and pulls the character down quicker, avoiding floatiness.
        if (torsoRb.linearVelocity.y < 0f && useGravityForce) 
        {
            // Increase falling velocity based on current gravity multiplied by a gravity factor.
            torsoRb.linearVelocity -= Vector2.down * gravityForce * Physics.gravity.y * Time.fixedDeltaTime;
        }

        /*
        // If the character is grounded, we can reset the jump count to allow for more jumping! Replaced with JumpScript.cs.
        if (IsGrounded() && !grounded)
        {
            grounded = true;
            jumpCount = 1;
        }
        */
    }

    // This function checks if the player is standing on a platform marked as "Ground", but was replaced with JumpScript.cs.
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);
    }

    // This function flips the character to face in the opposite x-direction.
    private void Flip()
    {
        if (doFlip)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // Toggles and flips character x-direction based on current x-direction.
            transform.localScale = localScale; // Apply flip!
        }
    }

    //Check if player hit GameOver barriers
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("GameOver"))
        {
            Debug.Log("Player should respawn!");
            respawnScript.Respawn();
        }
    }

    #region Input Actions

    // This function is called by the Player Input component, when "Move" input actions are triggered.
    public void Move(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x; // Returns value based on left (-1) and right (1) movement.
    }

    // This function is called by the Player Input component, when "Jump" input actions are triggered.
    public void Jump(InputAction.CallbackContext context)
    {
        // This if-statement checks if the jump button is pressed while the has refuelled their allowed jumps (from being grounded).
        if(context.performed && canJump && jumpCount < maxJumps)
        {
            // Apply an upwards velocity to the character based on jumpHeight.
            torsoRb.linearVelocity = new Vector2(torsoRb.linearVelocity.x, jumpHeight);
            sfxEvent.Invoke(jumpSound);

            grounded = false;

            jumpCount++;
        }

        // This if-statement checks if the jump button is released, so that by holding the jump button longer, the character-
        // -jumps higher.
        if(context.canceled && torsoRb.linearVelocity.y > 0f && holdToJumpHigher)
        {
            // Reduce current y-velocity by half to reduce jump height on jump button released.
            torsoRb.linearVelocity = new Vector2(torsoRb.linearVelocity.x, torsoRb.linearVelocity.y * 0.5f);
        }
    }

    #endregion

    // This function is called by the JumpScript.cs script, when the puppet collides with anything that is a Ground Layer.
    public void ResetJump()
    {
        grounded = true;
        jumpCount = 0;
    }
}
