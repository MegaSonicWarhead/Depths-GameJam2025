using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    private int money = 0;

    // Method to add money
    public void AddMoney(int amount)
    {
        money += amount;
    }

    // Method to get the current amount of money
    public int GetMoney()
    {
        return money;
    }
}
