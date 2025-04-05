using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{

    public Slider healthSlider;      // Reference to the health slider UI
    public GameObject gameOverPanel; // Reference to the Game Over panel UI
    public TextMeshProUGUI gameOverText; // Reference to the Game Over text
    public Button retryButton;       // Reference to the retry button
    public Button exitButton;        // Reference to the exit button

    public float maxHealth = 100f;   // Maximum health of the player
    private float currentHealth;     // Current health of the player
    public bool isDead = false;      // Flag to check if the player is dead

    void Start()
    {
        currentHealth = maxHealth; // Set the current health to max at the start
        UpdateHealthSlider();       // Update the health slider at the start

        // Make sure the Game Over panel is hidden initially
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Attach listeners to the buttons
        if (retryButton != null) retryButton.onClick.AddListener(Retry);
        if (exitButton != null) exitButton.onClick.AddListener(Exit);
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))  // Press T to toggle panel visibility
    //    {
    //        if (gameOverPanel != null)
    //        {
    //            bool isActive = gameOverPanel.activeSelf;
    //            gameOverPanel.SetActive(!isActive);  // Toggle visibility
    //            Debug.Log("Toggling Game Over Panel visibility: " + !isActive);
    //        }
    //    }
    //}

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
                TriggerGameOver();
            }
        }
    }

    // Trigger Game Over UI
    private void TriggerGameOver()
    {
        if (gameOverPanel != null)
        {
            
          gameOverPanel.SetActive(true);  // Show the Game Over panel
            Debug.Log("Game Over Panel activated!");
        }
        else
        {
            Debug.LogWarning("Game Over Panel is not assigned in the Inspector!");
        }

        // Temporarily remove time scale for testing
        // Time.timeScale = 0f;

        if (gameOverText != null)
        {
            gameOverText.text = "Game Over";
        }
    }

    // Retry the game (reset health and resume)
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
        Application.Quit(); // Exit the game (works in a build)
    }
}
