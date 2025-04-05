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
        rb.isKinematic = true;  // Set to Kinematic if we're manually moving the enemy
        Debug.Log("Enemy started patrolling");

        player = GameObject.FindGameObjectWithTag(targetTag)?.transform;

        // Attempt to find the player's HealthBar component if not assigned
        if (playerHealth == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag(targetTag);
            if (playerObject != null)
            {
                playerHealth = playerObject.GetComponent<HealthBar>();
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
                PatrolMovement();
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

        Transform targetPatrolPoint = movingToB ? patrolPointB : patrolPointA;

        transform.position = Vector2.MoveTowards(transform.position, targetPatrolPoint.position, patrolSpeed * Time.deltaTime);

        if (transform.position == targetPatrolPoint.position)
        {
            movingToB = !movingToB;  // Toggle the direction for the next patrol
        }

        Debug.DrawLine(transform.position, targetPatrolPoint.position, Color.red);
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
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
