using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{

    public Slider healthSlider;        // Reference to the health slider UI
    public GameObject gameOverPanel;   // Game Over panel UI reference
    public TextMeshProUGUI gameOverText; // Game Over text reference
    public Button retryButton;         // Retry button reference
    public Button exitButton;          // Exit button reference

    public float maxHealth = 100f;     // Maximum health of the player
    private float currentHealth;       // Current health of the player
    public bool isDead = false;        // Flag to check if the player is dead

    void Start()
    {
        currentHealth = maxHealth; // Set the current health to max at the start
        UpdateHealthSlider();       // Update health slider UI at the start

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // Hide Game Over panel initially
        }

        if (retryButton != null) retryButton.onClick.AddListener(Retry);
        if (exitButton != null) exitButton.onClick.AddListener(Exit);
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
        if (isDead) return;

        if (currentHealth > 0)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHealthSlider(); // Update slider after health changes

            if (currentHealth <= 0)
            {
                isDead = true;
                TriggerGameOver();
            }
        }
    }

    // Trigger the Game Over UI
    private void TriggerGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);  // Show the Game Over panel
            Debug.Log("Game Over Panel activated!");
        }

        if (gameOverText != null)
        {
            gameOverText.text = "Game Over";
        }
    }

    // Retry button callback
    public void Retry()
    {
        isDead = false;
        currentHealth = maxHealth;  // Reset health to max
        UpdateHealthSlider();       // Update the health slider
        gameOverPanel.SetActive(false);  // Hide the Game Over panel
        Time.timeScale = 1f;        // Resume the game
    }

    // Exit the game
    public void Exit()
    {
        Debug.Log("Exiting Game...");
        Application.Quit(); // Quit the game (only works in builds)
    }
}
