using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{

    public Slider healthSlider;        // Reference to the health slider UI
    public float maxHealth = 100f;     // Maximum health of the player
    private float currentHealth;       // Current health of the player
    public bool isDead = false;        // Flag to check if the player is dead

    void Start()
    {
        currentHealth = maxHealth; // Set the current health to max at the start
        UpdateHealthSlider();       // Update health slider UI at the start
    }

    // Method to update the health slider
    public void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            // Normalize health value to be between 0 and 1 for the slider
            float normalizedHealth = Mathf.Clamp(currentHealth / maxHealth, 0f, 1f);
            healthSlider.value = normalizedHealth;
            // Force the canvas to update the slider immediately
            Canvas.ForceUpdateCanvases();  // Force UI update after slider value change

            // Debugging the normalized value
            Debug.Log("Health slider normalized value: " + normalizedHealth);

            // Check if health is zero and trigger game over
            if (currentHealth <= 0f && !isDead)
            {
                isDead = true;
                TriggerGameOver();  // Trigger game over sequence
            }
        }
        else
        {
            Debug.LogWarning("Health Slider is not assigned in the Inspector!");
        }
    }

    // Method to decrease health
    public void DepleteHealth(float damage)
    {
        // Decrease health by damage amount
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);  // Ensure health doesn't go below zero

        // Add a small delay between updates (for testing purposes or smoother UI)
        StartCoroutine(UpdateHealthWithDelay());
    }

    private IEnumerator UpdateHealthWithDelay()
    {
        // Wait for 0.1 seconds before updating the slider
        yield return new WaitForSeconds(0.1f);
        UpdateHealthSlider();  // Update the health slider after the delay
    }

    // Trigger the Game Over UI or state when health reaches zero
    private void TriggerGameOver()
    {
        Debug.Log("Player is dead. Triggering game over.");
        // For now, we'll just load the Game Over scene after a short delay
        Invoke("LoadGameOverScene", 2f); // Delay by 2 seconds before transitioning to the Game Over scene
    }

    // Method to load the Game Over scene
    private void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");  // Replace "GameOver" with the actual scene name for Game Over
    }
}
