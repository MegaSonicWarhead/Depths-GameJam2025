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
        // Getting input for left-right movement (A/D for movement)
        float moveHorizontal = 0f;
        if (Input.GetKey(KeyCode.A)) // Press A to move left
        {
            moveHorizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.D)) // Press D to move right
        {
            moveHorizontal = 1f;
        }

        // Getting input for up (W key) or down (S key)
        float moveVertical = 0f;
        if (Input.GetKey(KeyCode.W))  // Press W to swim up
        {
            moveVertical = swimUpSpeed;
        }
        else if (Input.GetKey(KeyCode.S)) // Press S to swim down
        {
            moveVertical = -swimDownSpeed;
        }

        // Combine movement input into a vector
        moveInput = new Vector2(moveHorizontal, moveVertical);
    }

    void FixedUpdate()
    {
        // Apply gravity manually if the player is not swimming up/down
        if (moveInput.y == 0f)  // Only apply gravity if not swimming up/down
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y - gravity);  // Apply gravity to Y-axis
        }
        else
        {
            // Apply swimming up/down speed
            rb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y);
        }
    }

    private void HandleOxygenDepletion()
    {
        // Only deplete oxygen if the player is underwater
        if (oxygenBar != null && !IsPlayerOnSurface())
        {
            oxygenBar.DepleteOxygen(Time.deltaTime);  // Deplete oxygen each frame
        }

        // If oxygen runs out, start losing health
        if (oxygenBar != null && oxygenBar.GetCurrentOxygen() <= 0)
        {
            healthBar.DepleteHealth(Time.deltaTime * 5f); // Slowly lose health if out of oxygen
        }
    }

    private bool IsPlayerOnSurface()
    {
        float surfaceLevel = 0f;  // Surface level assumed to be y = 0
        return transform.position.y >= surfaceLevel;
    }
}
