using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralScript : MonoBehaviour
{
    // Reference to the player's OxygenBar
    public OxygenBar oxygenBar;
    public AudioManager audioManager; 

    // When the player enters the coral's trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Check if the colliding object is the player
        {
            // Set the player's oxygen to maximum
            if (oxygenBar != null)
            {
                oxygenBar.RefillOxygen(100f);  // Refills the oxygen to maximum (100)
                Debug.Log("Oxygen refilled to 100!");
            }
            else
            {
                Debug.LogWarning("OxygenBar reference is not assigned in the Inspector!");
            }

            if (audioManager != null)
            {
                audioManager.PlayBubble();  // Play the bubble sound effect
            }
            else
            {
                Debug.LogWarning("AudioManager is not assigned in the Inspector!");
            }
        }
    }
}
