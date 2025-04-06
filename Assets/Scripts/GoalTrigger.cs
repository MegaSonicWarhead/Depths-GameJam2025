using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    // This method is called when another collider enters the trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that collided has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Load the "Win" scene
            SceneManager.LoadScene("Win");
        }
    }
}
