using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damageAmount = 50f;           // The amount of damage the enemy deals
    public float damageInterval = 1f;          // How often the enemy damages the player
    private float timeSinceLastDamage = 0f;    // Timer to track damage intervals

    public Transform patrolPointA;             // First patrol point
    public Transform patrolPointB;             // Second patrol point
    public float patrolSpeed = 3f;             // Speed at which the enemy moves between patrol points
    private bool movingToB = true;             // Direction of movement
    private HealthBar playerHealth;            // Reference to the player's health bar
    private Transform player;                  // Reference to the player

    public float detectionRange = 250f;        // The distance at which the enemy will move towards the player

    private float startY;                      // The starting Y position for the enemy to keep it from falling

    void Start()
    {
        // Initialize references
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        playerHealth = player?.GetComponent<HealthBar>();

        // Ensure player exists in the scene
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        // Save the starting Y position to avoid falling off platforms
        startY = transform.position.y;
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate distance from the player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // If player is within detection range, move towards them
            if (distanceToPlayer <= detectionRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                // Otherwise, patrol between two points
                PatrolMovement();
            }

            // Keep track of the time passed since last damage
            if (timeSinceLastDamage < damageInterval)
            {
                timeSinceLastDamage += Time.deltaTime;
            }
        }

        // Ensure the enemy stays at the starting Y position (so it doesn't fall)
        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(currentPosition.x, startY, currentPosition.z);
    }

    private void PatrolMovement()
    {
        // Ensure patrol points are assigned before proceeding
        if (patrolPointA != null && patrolPointB != null)
        {
            // Determine which patrol point to move towards
            Transform targetPatrolPoint = movingToB ? patrolPointB : patrolPointA;

            // Only move on the X-axis (for horizontal patrol)
            Vector3 targetPosition = new Vector3(targetPatrolPoint.position.x, startY, transform.position.z);

            // Move the enemy towards the target patrol point
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);

            // If the enemy reaches the patrol point, switch direction
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                movingToB = !movingToB;  // Switch direction between points A and B
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Move the enemy towards the player's position
            float step = patrolSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.position, step);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // When the player is in range, apply damage at intervals
        if (other.CompareTag("Player") && timeSinceLastDamage >= damageInterval)
        {
            Debug.Log("Player in range, applying damage");
            playerHealth.DepleteHealth(damageAmount);  // Apply damage to the player
            timeSinceLastDamage = 0f;  // Reset the damage timer
        }
    }
}
