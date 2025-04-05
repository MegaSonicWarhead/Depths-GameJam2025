using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider healthSlider;   // Reference to the health slider UI
    public float maxHealth = 100f; // Maximum health of the player
    private float currentHealth;  // Current health of the player
    public bool isDead = false;  // Flag to check if the player is dead

    void Start()
    {
        currentHealth = maxHealth; // Set the current health to max at the start
        UpdateHealthSlider();      // Update the health slider at the start
    }

    // Method to update the health slider
    public void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            float normalizedHealth = Mathf.Clamp(currentHealth / maxHealth, 0f, 1f);
            healthSlider.value = normalizedHealth;
        }
        else
        {
            Debug.LogWarning("Health Slider is not assigned in the Inspector!");
        }
    }

    // Method to decrease health
    public void DepleteHealth(float amount)
    {
        if (isDead) // Stop any health updates if the player is dead
        {
            Debug.Log("Player is already dead. No further health updates.");
            return;
        }

        if (currentHealth > 0)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHealthSlider(); // Update the health slider immediately after health is updated

            if (currentHealth <= 0)
            {
                isDead = true;
                Debug.Log("Player has died.");
            }
        }
    }
}
