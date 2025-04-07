using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 70f;            // Speed of the player movement (left/right)
    public float swimUpSpeed = 70f;         // Speed at which the player swims upward
    public float swimDownSpeed = 70f;       // Speed at which the player swims downward
    public float gravity = 0.1f;            // Custom gravity to pull the player down
    public AudioManager audioManager; // Reference to the AudioManager for sound effects
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // References to health and oxygen systems
    public HealthBar healthBar;
    public OxygenBar oxygenBar;
    private bool isDiving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;  // Disable default gravity to apply custom gravity
    }

    void Update()
    {
        HandleMovementInput();
        HandleOxygenDepletion();
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

        //Diving sound trigger
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && !isDiving)
        {
            isDiving = true;
            if (audioManager != null)
            {
                audioManager.PlayDiving();
                Debug.Log("Diving sound played");
            }
        }
        else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            isDiving = false;
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement as before
        float moveX = moveInput.x * moveSpeed;

        // Apply vertical movement (swim up or down) and gravity
        float moveY = moveInput.y - gravity;  // Combine swim movement with gravity

        // Set the velocity for both horizontal and vertical movement
        rb.velocity = new Vector2(moveX, moveY);
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

            if (audioManager != null)
            {
                audioManager.PlayDrowning();
                Debug.Log("Drowning sound played");
            }

        }
        if (oxygenBar != null && oxygenBar.GetCurrentOxygen() <= 0)
        {
            healthBar.DepleteHealth(Time.deltaTime * 5f); // Slowly lose health if out of oxygen
            if (audioManager != null)
            {
                audioManager.PlayDrowning();
                Debug.Log("Drowning sound played");
            }
        }
        
    }

    private bool IsPlayerOnSurface()
    {
        return transform.position.y >= 0;  // Surface is at y = 0
    }
}
