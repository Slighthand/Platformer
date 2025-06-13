using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class ShopManager : MonoBehaviour
{
    public GameObject player;
    public Transform shopPanel; // The right-side UI panel
    public GameObject PowerUpsUIPrefab;

    public Sprite heartIcon, speedIcon, shieldIcon;

    private List<PowerUps> powerUpsList = new List<PowerUps>();

    void Start()
    {
        // Composition: the manager owns the items
        powerUpsList.Add(new ExtraHeart(heartIcon));
        powerUpsList.Add(new SpeedBoost(speedIcon));
        powerUpsList.Add(new Shield(shieldIcon));

        SortPowerUps();
        DisplayShop();
    }

    public void SortPowerUps()
    {
        // Alphabetical sort
        powerUpsList.Sort((a, b) => a.Name.CompareTo(b.Name));
    }

    public PowerUps SearchPowerUp(string name)
    {
        foreach (var item in powerUpsList)
        {
            if (item.Name.ToLower() == name.ToLower())
                return item;
        }
        return null;
    }

    void DisplayShop()
    {
        foreach (var item in powerUpsList)
        {
            GameObject ui = Instantiate(PowerUpsUIPrefab, shopPanel);
            ShopItemUI shopUI = ui.GetComponent<ShopItemUI>();
            shopUI.Setup(item, this);
        }
    }

    public bool TryBuy(PowerUps item)
    {
        if (CoinManager.CoinCount >= item.Cost)
        {
            CoinManager.CoinCount -= item.Cost;
            item.Owned++;
            item.ApplyEffect(player);
            return true;
        }
        return false;
    }
}
