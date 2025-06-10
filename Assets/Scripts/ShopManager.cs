using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public Transform shopUIParent; 
    public GameObject shopItemPrefab;
    public GameObject player;
    public Text coinText;

    public int playerCoins = 150;

    private List<PowerUps> powerUp = new List<PowerUps>();

    void Start()
    {
        // Load Sprites 
        Sprite speedIcon = Resources.Load<Sprite>("Icons/speed");
        Sprite heartIcon = Resources.Load<Sprite>("Icons/heart");
        Sprite shieldsIcon = Resources.Load<Sprite>("Icons/shield");

        powerUp.Add(new SpeedBoost(speedIcon));
        powerUp.Add(new ExtraHeart(heartIcon));
        powerUp.Add(new Shields(shieldsIcon));

       //Sorting alphabetically!
        powerUp.Sort((a, b) => a.Name.CompareTo(b.Name));

        DisplayShop();
    }

    void DisplayShop()
    {
        coinText.text = $"Coins: {playerCoins}";

        foreach (var p in powerUp)
        {
            GameObject item = Instantiate(shopItemPrefab, shopUIParent);
            item.transform.Find("Name").GetComponent<Text>().text = p.Name;
            item.transform.Find("Icon").GetComponent<Image>().sprite = p.Icon;
            item.transform.Find("Cost").GetComponent<Text>().text = $"Cost: {p.Cost}";
            item.transform.Find("Owned").GetComponent<Text>().text = $"x{p.Quantity}";
            Button buyBtn = item.transform.Find("BuyButton").GetComponent<Button>();

            buyBtn.onClick.AddListener(() =>
            {
                TryBuyPowerUps(p);
                item.transform.Find("Owned").GetComponent<Text>().text = $"x{p.Quantity}";
                coinText.text = $"Coins: {playerCoins}";
            });
        }
    }

    void TryBuyPowerUp(PowerUps p)
    {
        if (playerCoins >= p.Cost)
        {
            playerCoins -= p.Cost;
            p.Quantity++;
            p.ApplyEffect(player);
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}
