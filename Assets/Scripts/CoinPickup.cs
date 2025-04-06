using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinPickup : MonoBehaviour
{
    public TextMeshProUGUI moneyText;  // Reference to the UI Text to display money
    private PlayerMoney playerMoney;   // Reference to the PlayerMoney script

    void Start()
    {
        // Find the PlayerMoney script on the player object
        playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>();

        // Debug log to ensure playerMoney is correctly assigned
        if (playerMoney == null)
        {
            Debug.LogError("PlayerMoney script not found on the Player object.");
        }

        // Debug log to ensure moneyText is correctly assigned
        if (moneyText == null)
        {
            Debug.LogError("MoneyText reference is not assigned in the Inspector.");
        }
    }

    // This method is called when another collider enters the trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered with: " + other.gameObject.name);  // Check what triggers it

        // Check if the object that collided is the player (has the "Player" tag)
        if (other.CompareTag("Player"))
        {
            // Add money to the player using the PlayerMoney script
            playerMoney.AddMoney(1);

            // Update the UI text to show the current money (from the PlayerMoney script)
            moneyText.text = "$: " + playerMoney.GetMoney();

            // Debug log to verify the money is being updated
            Debug.Log("Coin picked up! Current Money: " + playerMoney.GetMoney());

            // Destroy the coin object after picking it up
            Destroy(gameObject);
        }
    }
}
