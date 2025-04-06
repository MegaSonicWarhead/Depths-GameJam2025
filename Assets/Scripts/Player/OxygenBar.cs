using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    public Slider oxygenSlider;   // Reference to the oxygen slider UI
    public float maxOxygen = 100f; // Maximum oxygen level
    private float currentOxygen;  // Current oxygen level

    public float oxygenDepletionRate = 5f; // Rate of oxygen depletion per second when underwater

    void Start()
    {
        currentOxygen = maxOxygen; // Set initial oxygen to max
        UpdateOxygenSlider();      // Update the oxygen slider at the start
    }

    // Method to update the oxygen slider
    public void UpdateOxygenSlider()
    {
        if (oxygenSlider != null)
        {
            oxygenSlider.value = currentOxygen / maxOxygen;  // Normalize oxygen value
        }
        else
        {
            Debug.LogWarning("Oxygen Slider is not assigned in the Inspector!");
        }
    }

    // Method to deplete oxygen
    public void DepleteOxygen(float amount)
    {
        currentOxygen -= amount;
        currentOxygen = Mathf.Clamp(currentOxygen, 0, maxOxygen); // Ensure oxygen doesn't go below 0
        UpdateOxygenSlider(); // Update the slider UI
    }

    // Method to refill oxygen
    public void RefillOxygen(float amount)
    {
        currentOxygen += amount;
        currentOxygen = Mathf.Clamp(currentOxygen, 0, maxOxygen); // Ensure oxygen doesn't exceed max
        UpdateOxygenSlider(); // Update the slider UI
    }

    // Get the current oxygen level
    public float GetCurrentOxygen()
    {
        return currentOxygen;
    }
}
