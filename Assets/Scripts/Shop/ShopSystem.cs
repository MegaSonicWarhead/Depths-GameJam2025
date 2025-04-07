using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public GameObject shopUI;
    private bool playerInRange = false;

    public HealthBar playerHealthBar;
    public float upgradeAmount = 20f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleShopUI();
        }
    }
    private void ToggleShopUI()
    {
        /*bool isActive = shopUI.activeSelf;
        shopUI.SetActive(!isActive);

        if (!isActive)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }*/
        bool isActive = shopUI.activeSelf;
        shopUI.SetActive(!isActive);
        Time.timeScale = isActive ? 1f : 0f;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            shopUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void BuyHealth()
    {
        if (playerHealthBar != null)
        {
            playerHealthBar.UpgradeMaxHealth(upgradeAmount);
        }
    }
}
