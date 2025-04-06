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
    public float patrolSpeed = 3f;             // Speed at which the enemy moves between patrol points (adjusted for visibility)
    private bool movingToB = true;             // Direction of movement
    private HealthBar playerHealth;            // Reference to the player's health bar
    private Transform player;                  // Reference to the player

    // Public variable to set the detection distance manually
    public float detectionRange = 250f;          // The distance at which the enemy will move towards the player

    void Start()
    {
        // Find the player and health bar components at the start
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        playerHealth = player?.GetComponent<HealthBar>();

        // Debug log to ensure the player is found
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the distance between the enemy and the player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Debug log to show distance
            //Debug.Log("Distance to player: " + distanceToPlayer);

            // Move towards the player if within the set detection range
            if (distanceToPlayer <= detectionRange)
            {
                Debug.Log("Player within range. Moving towards player.");
                MoveTowardsPlayer();
            }
            else
            {
                PatrolMovement(); // Continue patrolling if out of range
            }

            // Handle damage interval logic
            if (timeSinceLastDamage < damageInterval)
            {
                timeSinceLastDamage += Time.deltaTime;
            }
        }
    }

    private void PatrolMovement()
    {
        // If patrol points are assigned, patrol between them
        if (patrolPointA != null && patrolPointB != null)
        {
            Transform targetPatrolPoint = movingToB ? patrolPointB : patrolPointA;

            // Move towards the target patrol point
            transform.position = Vector2.MoveTowards(transform.position, targetPatrolPoint.position, patrolSpeed * Time.deltaTime);

            // Check if the enemy has reached the patrol point, then reverse direction
            if (Vector2.Distance(transform.position, targetPatrolPoint.position) < 0.1f)
            {
                movingToB = !movingToB;
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        // Move towards the player at the patrol speed
        if (player != null)
        {
            float step = patrolSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.position, step);
            //xDebug.Log("Moving towards player. Current position: " + transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // If the enemy is colliding with the player and the damage interval is passed, apply damage
        if (other.CompareTag("Player") && timeSinceLastDamage >= damageInterval)
        {
            playerHealth.DepleteHealth(damageAmount);
            timeSinceLastDamage = 0f;  // Reset the damage timer
        }
    }
}
