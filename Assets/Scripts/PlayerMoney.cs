using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    private int money = 0;

    public TextMeshProUGUI coinText;

    // Method to add money
    public void AddMoney(int amount)
    {
        money += amount;
        money = Mathf.Max(0, money);
        UpdateUI();
    }

    // Method to get the current amount of money
    public int GetMoney()
    {
        return money;
    }

    private void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = "$: " + money;
        }
        
    }
}
