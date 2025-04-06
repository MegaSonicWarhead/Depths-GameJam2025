using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    // Name of the game scene to load (you should replace "Game" with the exact name of your game scene)
    public string gameSceneName = "Game";  // Set this in the inspector if you want to change the scene name

    // Method to be called when the retry button is clicked
    public void Retry()
    {
        // Load the "Game" scene to restart the game
        SceneManager.LoadScene(gameSceneName);
    }
}
