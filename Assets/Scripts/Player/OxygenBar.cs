using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    public Slider oxygenSlider;   // Reference to the oxygen slider UI
    public float maxOxygen = 100f; // Maximum oxygen level
    private float currentOxygen;  // Current oxygen level

    void Start()
    {
        currentOxygen = maxOxygen; // Set the current oxygen to max at the start
        UpdateOxygenSlider();      // Update the oxygen slider at the start
    }

    // Method to update the oxygen slider
    public void UpdateOxygenSlider()
    {
        if (oxygenSlider != null)
        {
            oxygenSlider.value = currentOxygen / maxOxygen;  // Set slider value based on current oxygen
        }
        else
        {
            Debug.LogWarning("Oxygen Slider is not assigned in the Inspector!");
        }
    }

    // Method to deplete oxygen (e.g., over time or in response to player actions)
    public void DepleteOxygen(float amount)
    {
        currentOxygen -= amount;
        currentOxygen = Mathf.Clamp(currentOxygen, 0, maxOxygen); // Ensure oxygen doesn't go below 0
        UpdateOxygenSlider(); // Update the oxygen slider
    }

    // Method to refill oxygen
    public void RefillOxygen(float amount)
    {
        currentOxygen += amount;
        currentOxygen = Mathf.Clamp(currentOxygen, 0, maxOxygen); // Ensure oxygen doesn't exceed max
        UpdateOxygenSlider(); // Update the oxygen slider
    }

    // Method to get the current oxygen level
    public float GetCurrentOxygen()
    {
        return currentOxygen;
    }
}
