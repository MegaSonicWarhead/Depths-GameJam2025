using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 70f;            // Speed of the player movement (left/right)
    public float swimUpSpeed = 70f;         // Speed at which the player swims upward
    public float gravity = 0.1f;            // Custom gravity to pull the player down
    public float swimDownSpeed = 70f;       // Speed at which the player swims downward

    private Rigidbody2D rb;
    private Vector2 moveInput;

    // References to health and oxygen systems
    public HealthBar healthBar;
    public OxygenBar oxygenBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component
        rb.gravityScale = 0;  // Disable default gravity to apply custom gravity
    }

    void Update()
    {
        HandleMovementInput();  // Handles user input for movement
        HandleOxygenDepletion(); // Depletes oxygen and health if necessary
    }

    private void HandleMovementInput()
    {
        float moveHorizontal = 0f;
        if (Input.GetKey(KeyCode.A)) moveHorizontal = -1f;
        if (Input.GetKey(KeyCode.D)) moveHorizontal = 1f;

        float moveVertical = 0f;
        if (Input.GetKey(KeyCode.W)) moveVertical = swimUpSpeed;
        if (Input.GetKey(KeyCode.S)) moveVertical = -swimDownSpeed;

        moveInput = new Vector2(moveHorizontal, moveVertical);
    }

    void FixedUpdate()
    {
        /*if (moveInput.y == 0f)
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y - gravity);  // Apply gravity to Y-axis
        }
        else
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y);
        }*/
        Vector2 currentPosition = rb.position;
        Vector2 targetVelocity;

        if (moveInput.y == 0f)
        {
            // Gravity pull
            targetVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y - gravity);
        }
        else
        {
            // Swimming
            targetVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y);
        }

        // Apply movement with collision-safe method
        Vector2 newPosition = currentPosition + targetVelocity * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    private void HandleOxygenDepletion()
    {
        if (oxygenBar != null && !IsPlayerOnSurface())
        {
            oxygenBar.DepleteOxygen(Time.deltaTime);  // Deplete oxygen each frame
        }

        if (oxygenBar != null && oxygenBar.GetCurrentOxygen() <= 0)
        {
            healthBar.DepleteHealth(Time.deltaTime * 5f); // Slowly lose health if out of oxygen
        }
    }

    private bool IsPlayerOnSurface()
    {
        return transform.position.y >= 0;  // Surface assumed to be y = 0
    }
}
