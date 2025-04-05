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
            healthSlider.value = currentHealth / maxHealth;  // Set slider value based on current health
        }
        else
        {
            Debug.LogWarning("Health Slider is not assigned in the Inspector!");
        }
    }

    // Method to decrease health
    public void DepleteHealth(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Ensure health doesn't go below 0
        UpdateHealthSlider(); // Update the health slider

        if (currentHealth <= 0)
        {
            // Handle player death (e.g., trigger game over)
            Debug.Log("Player has died.");
        }
    }

    // Method to heal the player
    //public void HealPlayer(float amount)
    //{
    //    currentHealth += amount;
    //    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Ensure health doesn't exceed max health
    //    UpdateHealthSlider(); // Update the health slider
    //}
}
