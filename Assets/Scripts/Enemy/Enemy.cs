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
    public float patrolSpeed = 150f;           // Speed at which the enemy moves between patrol points
    private bool movingToB = true;             // Direction of movement
    private HealthBar playerHealth;            // Reference to the player's health bar
    private Transform player;                  // Reference to the player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        playerHealth = player?.GetComponent<HealthBar>();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= 5f) // Adjust detection range
            {
                MoveTowardsPlayer();
            }
            else
            {
                PatrolMovement(); // Continue patrolling
            }

            if (timeSinceLastDamage < damageInterval)
            {
                timeSinceLastDamage += Time.deltaTime;
            }
        }
    }

    private void PatrolMovement()
    {
        if (patrolPointA == null || patrolPointB == null) return;

        Transform targetPatrolPoint = movingToB ? patrolPointB : patrolPointA;
        transform.position = Vector2.MoveTowards(transform.position, targetPatrolPoint.position, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPatrolPoint.position) < 0.1f)
        {
            movingToB = !movingToB;
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            float step = patrolSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.position, step);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && timeSinceLastDamage >= damageInterval)
        {
            playerHealth.DepleteHealth(damageAmount);
            timeSinceLastDamage = 0f;
        }
    }
}
