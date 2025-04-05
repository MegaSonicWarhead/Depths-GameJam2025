using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    public float maxOxygen = 100f;   // Max oxygen of the player
    public float currentOxygen;      // Current oxygen level of the player
    public Slider oxygenSlider;      // Reference to the oxygen UI slider
    public float oxygenDepletionRate = 1f; // Rate at which oxygen decreases

    void Start()
    {
        currentOxygen = maxOxygen;    // Set initial oxygen to max
        UpdateOxygenBar();            // Update the oxygen bar at the start
    }

    // Depletes oxygen over time
    public void DepleteOxygen(float amount)
    {
        currentOxygen -= amount * oxygenDepletionRate;
        currentOxygen = Mathf.Clamp(currentOxygen, 0, maxOxygen);  // Ensure oxygen doesn't go below 0
        UpdateOxygenBar();  // Update the oxygen UI bar
    }

    // Method to refill oxygen when the player surfaces
    public void RefillOxygen(float amount)
    {
        currentOxygen += amount;
        currentOxygen = Mathf.Clamp(currentOxygen, 0, maxOxygen);  // Ensure oxygen doesn't exceed max
        UpdateOxygenBar();  // Update the oxygen UI bar
    }

    // Update the oxygen bar UI
    private void UpdateOxygenBar()
    {
        oxygenSlider.value = currentOxygen / maxOxygen;  // Set slider value based on current oxygen
    }

    // Get current oxygen level (for other scripts to check)
    public float GetCurrentOxygen()
    {
        return currentOxygen;
    }
}
