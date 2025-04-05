using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damageAmount = 10f;            // The amount of damage the enemy deals
    public float damageInterval = 1f;           // How often the enemy damages the player (in seconds)
    public string targetTag = "Player";         // Tag of the target (e.g., "Player", can be adjusted if needed)
    private float timeSinceLastDamage = 0f;     // Timer to track damage intervals

    // Patrol points for enemy movement
    public Transform patrolPointA;             // The first patrol point
    public Transform patrolPointB;             // The second patrol point
    public float patrolSpeed = 2f;             // Speed at which the enemy moves between patrol points

    private bool movingToB = true;              // Direction of movement (true means moving towards patrolPointB)
    private HealthBar playerHealth;             // Reference to player HealthBar (automatically assigned or through inspector)

    private Rigidbody2D rb;

    void Start()
    {
        // Ensure Rigidbody2D is attached and set to Kinematic if not using physics
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on the enemy. Please add a Rigidbody2D component.");
            return;
        }
        rb.isKinematic = true; // Set to Kinematic if we're manually moving the enemy
        Debug.Log("Enemy started patrolling");

        // Attempt to find the player's HealthBar component if not assigned
        if (playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(targetTag);
            if (player != null)
            {
                playerHealth = player.GetComponent<HealthBar>();
            }
        }
    }

    void Update()
    {
        // Move the enemy between patrol points
        PatrolMovement();

        // Increment the time since the last damage was applied
        if (timeSinceLastDamage < damageInterval)
        {
            timeSinceLastDamage += Time.deltaTime;
        }
    }

    // Method to move the enemy between two patrol points
    private void PatrolMovement()
    {
        // Check if patrol points are assigned
        if (patrolPointA == null || patrolPointB == null)
        {
            Debug.LogError("Patrol points A and/or B are not assigned in the inspector.");
            return;
        }

        Transform targetPatrolPoint = movingToB ? patrolPointB : patrolPointA;

        // Move towards the current target patrol point
        transform.position = Vector2.MoveTowards(transform.position, targetPatrolPoint.position, patrolSpeed * Time.deltaTime);

        // If the enemy reaches the patrol point, switch direction
        if (transform.position == targetPatrolPoint.position)
        {
            movingToB = !movingToB;  // Toggle the direction for the next patrol
        }
    }

    // Detect collision with the player and apply damage
    private void OnTriggerStay2D(Collider2D other)
    {
        // Only apply damage if the enemy is touching the player (or the specified target)
        if (other.CompareTag(targetTag)) // Ensure it matches "Player" tag
        {
            if (timeSinceLastDamage >= damageInterval)
            {
                // Apply damage to player if health bar is assigned
                if (playerHealth != null)
                {
                    playerHealth.DepleteHealth(damageAmount);
                }

                // Reset the timer
                timeSinceLastDamage = 0f;
            }
        }
    }
}
