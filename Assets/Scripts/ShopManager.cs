using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public GameObject shopItemPrefab;
    public Transform shopPanel;
    public List<PowerUps> powerUps;
    public GameObject player;
    public int coins = 300;

    void Start()
    {
        foreach (var powerUp in powerUps)
        {
            GameObject item = Instantiate(shopItemPrefab, shopPanel);
            ShopItemUI ui = item.GetComponent<ShopItemUI>();
            ui.Setup(powerUp, this, player);
        }
    }

    public bool TryBuy(PowerUps powerUp)
    {
        if (coins >= powerUp.Cost)
        {
            coins -= powerUp.Cost;
            powerUp.Quantity++;
            powerUp.ApplyEffect(player);
            return true;
        }

        Debug.Log("Not enough coins!");
        return false;
    }
}