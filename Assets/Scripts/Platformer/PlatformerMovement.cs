using UnityEngine;
using UnityEngine.InputSystem; // We need to use this library to access our input actions!

/* This script adapts on to Bendux's 2D platformer movement script, using the new input system.
 * You can find the full tutorial here: https://www.youtube.com/watch?v=24-BkpFSZuI */
public class PlatformerMovement : MonoBehaviour
{
    // Local Variables
    private Rigidbody2D rb;
    private float horizontalInput;
    private bool facingRight = true;
    private Vector2 currentVelocity;

    // Local Variables - Player Movement Properties
    [Header("Movement Properties")]
    [Tooltip("The maximum speed for the character.")]
    [SerializeField] private float speed;
    [Tooltip("The maximum jump height for the character.")]
    [SerializeField] private float jumpHeight;
    [Tooltip("The speed at which the character falls.")]
    [SerializeField] private float gravityForce;

    // Public Variables
    [Header("Ground Check")]
    [Tooltip("An empty transform child positioned at the bottom of the character.")]
    public Transform groundCheck;
    [Tooltip("This is the layer(s) type which the character can jump on.")]
    public LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Finds local RigidBody2D component in the character.
    }

    private void FixedUpdate()
    {
        // Applies horizontal movement, with smooth acceleration and deceleration.
        float horizontalVel = horizontalInput * speed;
        float smoothVel = Mathf.SmoothDamp(rb.linearVelocity.x, horizontalVel, ref currentVelocity.x, 0.1f);
        rb.linearVelocity = new Vector2(smoothVel, rb.linearVelocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        // This if-statement flips the character's x-direction, to face in the direction of horizontal movement.
        if(!facingRight && horizontalInput > 0) // Facing left, but moving right?
        {
            Flip(); // Flip right!
        }
        else if(facingRight && horizontalInput < 0) // Facing right, but moving left?
        {
            Flip(); // Flip left!
        }

        // This if-statement checks if the character is falling, and pulls the character down quicker, avoiding floatiness.
        if (rb.linearVelocity.y < 0f) 
        {
            // Increase falling velocity based on current gravity multiplied by a gravity factor.
            rb.linearVelocity -= Vector2.down * gravityForce * Physics.gravity.y * Time.fixedDeltaTime;
        }
    }

    // This function checks if the player is standing on a platform marked as "Ground".
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // This function flips the character to face in the opposite x-direction.
    private void Flip()
    {
        facingRight = !facingRight; 
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f; // Toggles and flips character x-direction based on current x-direction.
        transform.localScale = localScale; // Apply flip!
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
        // This if-statement checks if the jump button is pressed while the character is "grounded".
        if(context.performed && IsGrounded())
        {
            // Apply an upwards velocity to the character based on jumpHeight.
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
        }

        // This if-statement checks if the jump button is released, so that by holding the jump button longer, the character-
        // -jumps higher.
        if(context.canceled && rb.linearVelocity.y > 0f)
        {
            // Reduce current y-velocity by half to reduce jump height on jump button released.
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    #endregion

}
