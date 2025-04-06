using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 70f;            // Speed of the player movement (left/right)
    public float swimUpSpeed = 70f;         // Speed at which the player swims upward
    public float swimDownSpeed = 70f;       // Speed at which the player swims downward
    public float gravity = 0.1f;            // Custom gravity to pull the player down

    private Rigidbody2D rb;
    private Vector2 moveInput;

    // References to health and oxygen systems
    public HealthBar healthBar;
    public OxygenBar oxygenBar;

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
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y - gravity);
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
        return transform.position.y >= 0;  // Surface is at y = 0
    }
}
