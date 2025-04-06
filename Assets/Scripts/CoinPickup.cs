using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinPickup : MonoBehaviour
{
    public TextMeshProUGUI moneyText;  // Reference to the UI Text to display money
    private int money = 0;  // Current money count

    // This method is called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided is the player (has the "Player" tag)
        if (other.CompareTag("Player"))
        {
            // Increase money by 1
            money += 1;

            // Update the UI text to show the current money
            moneyText.text = "Money: " + money;

            // Destroy the coin object after picking it up
            Destroy(gameObject);
        }
    }
}
