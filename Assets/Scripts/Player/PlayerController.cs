using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;          // Speed of the player movement (left/right)
    public float swimUpSpeed = 3f;       // Speed at which the player swims upward
    public float gravity = 0.1f;         // Custom gravity to pull the player down
    public float swimDownSpeed = 2f;     // Speed at which the player swims downward

    private Rigidbody2D rb;
    private Vector2 moveInput;

    // References to health and oxygen systems
    public HealthBar healthBar;
    public OxygenBar oxygenBar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        HandleOxygenDepletion();
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

        // Apply gravity manually (custom gravity)
        if (moveVertical == 0f) // Only apply gravity if not swimming up/down
        {
            rb.velocity += new Vector2(0, -gravity);  // Gravity pulls the player down
        }
    }

    // FixedUpdate is used for physics-related updates (better than Update for Rigidbody2D)
    void FixedUpdate()
    {
        // Apply the movement speed to the player
        Vector2 velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        // Apply the movement
        rb.velocity = velocity;
    }

    // Oxygen depletion (loss over time while underwater)
    private void HandleOxygenDepletion()
    {
        if (oxygenBar != null && !IsPlayerOnSurface())
        {
            oxygenBar.DepleteOxygen(Time.deltaTime); // Depletes oxygen over time if underwater
        }

        // If oxygen runs out, start losing health
        if (oxygenBar != null && oxygenBar.GetCurrentOxygen() <= 0)
        {
            healthBar.DepleteHealth(Time.deltaTime * 5f); // Slowly lose health if out of oxygen
        }
    }

    // Check if player is at the surface (we'll assume surface is y = 0)
    private bool IsPlayerOnSurface()
    {
        return transform.position.y >= 0; // If the player is at or above y = 0, they're on the surface
    }
}
