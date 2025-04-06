using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damageAmount = 50f;           // The amount of damage the enemy deals
    public float damageInterval = 1f;          // How often the enemy damages the player (in seconds)
    public string targetTag = "Player";        // Tag of the target (e.g., "Player", can be adjusted if needed)
    private float timeSinceLastDamage = 0f;    // Timer to track damage intervals

    // Patrol points for enemy movement
    public Transform patrolPointA;             // The first patrol point
    public Transform patrolPointB;             // The second patrol point
    public float patrolSpeed = 150f;           // Speed at which the enemy moves between patrol points

    private bool movingToB = true;             // Direction of movement (true means moving towards patrolPointB)
    private HealthBar playerHealth;            // Reference to player HealthBar (automatically assigned or through inspector)

    private Rigidbody2D rb;

    public float detectionRange = 5f;          // The distance at which the enemy starts moving towards the player
    private Transform player;                  // The player transform

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on the enemy. Please add a Rigidbody2D component.");
            return;
        }
        rb.isKinematic = true;

        player = GameObject.FindGameObjectWithTag(targetTag)?.transform;

        if (playerHealth == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag(targetTag);
            if (playerObject != null)
            {
                playerHealth = playerObject.GetComponent<HealthBar>();

                // 
                if (playerHealth != null)
                {
                    Debug.Log("Enemy found HealthBar on: " + playerHealth.gameObject.name);
                }
                else
                {
                    Debug.LogWarning("HealthBar component NOT found on the Player GameObject.");
                }
            }
        }

        if (playerHealth == null)
        {
            Debug.LogError("Player HealthBar not found! Please assign it properly.");
        }
    }


    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                PatrolMovement(); // Continue patrolling even if the player isn't detected
            }

            Debug.DrawLine(transform.position, player.position, Color.green);
        }

        if (timeSinceLastDamage < damageInterval)
        {
            timeSinceLastDamage += Time.deltaTime;
        }
    }

    private void PatrolMovement()
    {
        if (patrolPointA == null || patrolPointB == null)
        {
            Debug.LogError("Patrol points A and/or B are not assigned in the inspector.");
            return;
        }

        // Determine the target patrol point based on the direction (moving to B or moving to A)
        Transform targetPatrolPoint = movingToB ? patrolPointB : patrolPointA;

        // Move the enemy towards the target patrol point
        transform.position = Vector2.MoveTowards(transform.position, targetPatrolPoint.position, patrolSpeed * Time.deltaTime);

        // If the enemy has reached the patrol point, toggle the direction
        if (Vector2.Distance(transform.position, targetPatrolPoint.position) < 0.1f)
        {
            movingToB = !movingToB;  // Toggle the direction for the next patrol
        }

        // Debug lines for patrol points
        Debug.DrawLine(transform.position, targetPatrolPoint.position, Color.red);
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Move towards the player at patrol speed
            float step = patrolSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.position, step);
            Debug.DrawLine(transform.position, player.position, Color.blue);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(targetTag)) // Ensure it matches "Player" tag
        {
            if (timeSinceLastDamage >= damageInterval && playerHealth != null && !playerHealth.isDead)
            {
                playerHealth.DepleteHealth(damageAmount);
                timeSinceLastDamage = 0f;
            }
        }
    }
}
