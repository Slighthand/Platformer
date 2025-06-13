using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class ShopManager : MonoBehaviour
{
    public GameObject player;
    public Transform shopPanel; // The right-side UI panel
    public GameObject PowerUpsUIPrefab;

    public Sprite heartSprite, speedSprite, shieldSprite, bombSprite;

    [SerializeField] private List<PowerUps> powerUpsList = new List<PowerUps>();

    void Start() {
        powerUpsList.Clear();
        powerUpsList.Add(new ExtraHeart(heartSprite));
        powerUpsList.Add(new SpeedBoost(speedSprite));
        powerUpsList.Add(new Shield(shieldSprite));
        powerUpsList.Add(new BombPowerup(bombSprite));

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

    void DisplayShop() {
        print(powerUpsList.Count);
        foreach (PowerUps item in powerUpsList)
        {
            print(item.Name);
            GameObject ui = Instantiate(PowerUpsUIPrefab, shopPanel);
            ShopItemUI shopUI = ui.GetComponent<ShopItemUI>();
            shopUI.Setup(item, this, player);
        }
    }

    public bool TryBuy(PowerUps powerup)
    {
        if (CoinManager.CoinCount >= powerup.Cost)
        {
            CoinManager.CoinCount -= powerup.Cost;
            powerup.Owned++;
            powerup.ApplyEffect(player);
            return true;
        }
        return false;
    }
}
