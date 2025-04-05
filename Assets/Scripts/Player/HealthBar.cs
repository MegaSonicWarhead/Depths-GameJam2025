using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float maxHealth = 100f;    // Max health of the player
    public float currentHealth;       // Current health of the player
    public Slider healthSlider;       // Reference to the health UI slider

    void Start()
    {
        currentHealth = maxHealth;    // Set initial health to max
        UpdateHealthBar();            // Update the health bar at the start
    }

    void UpdateHealthBar()
    {
        healthSlider.value = currentHealth / maxHealth;  // Set slider value based on current health
    }

    // Method to deplete health over time (e.g., if oxygen is depleted)
    public void DepleteHealth(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Ensure health doesn't go below 0
        UpdateHealthBar();  // Update the health UI bar
    }

    // Method to heal the player
    public void HealPlayer(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Ensure health doesn't exceed max
        UpdateHealthBar();  // Update the health UI bar
    }
}
