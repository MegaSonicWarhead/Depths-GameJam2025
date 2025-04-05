using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Reference to the pause menu UI panel
    public Button resumeButton;     // Reference to the resume button
    public Button exitButton;       // Reference to the exit button
    public Button pauseButton;      // Reference to the pause button to trigger the pause

    private bool isPaused = false;  // Track if the game is paused

    void Start()
    {
        // Ensure the pause menu is hidden initially
        pauseMenuUI.SetActive(false);

        // Add listeners to buttons
        resumeButton.onClick.AddListener(Resume);
        exitButton.onClick.AddListener(Exit);
        pauseButton.onClick.AddListener(TogglePause);
    }

    void Update()
    {
        // Optional: Handle the pause through a key press (e.g., Escape)
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            TogglePause();
        }
    }

    // Toggle the pause menu (called when pause button is pressed or Escape is pressed)
    public void TogglePause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    // Show the pause menu and pause the game
    private void Pause()
    {
        pauseMenuUI.SetActive(true);    // Show the pause menu UI
        Time.timeScale = 0f;            // Pause the game (set time scale to 0)
        isPaused = true;                // Set paused state
    }

    // Hide the pause menu and resume the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);   // Hide the pause menu UI
        Time.timeScale = 1f;            // Resume the game (set time scale to 1)
        isPaused = false;               // Set not paused
    }

    // Exit the game (this will only work in a build)
    public void Exit()
    {
        Time.timeScale = 1f;            // Ensure game resumes before quitting
        Debug.Log("Exiting Game...");
        Application.Quit();             // Close the game
    }
}
