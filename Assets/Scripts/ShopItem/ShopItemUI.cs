using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public Image icon;
    public Text itemName;
    public Text cost;
    public Text owned;
    public Button buyButton;

    PowerUps powerUp;
    ShopManager shopManager;
    GameObject player;

    public void Setup(PowerUps powerUp, ShopManager shopManager, GameObject player)
    {
        this.powerUp = powerUp;
        this.shopManager = shopManager;
        this.player = player;

        icon.sprite = powerUp.Icon;
        itemName.text = powerUp.Name;
        cost.text = $"Cost: {powerUp.Cost}";
        UpdateOwned();

        buyButton.onClick.AddListener(BuyItem);
    }

    void BuyItem()
    {
        if (shopManager.TryBuy(powerUp))
        {
            UpdateOwned();
        }
    }

    void UpdateOwned()
    {
        owned.text = $"Owned: x{powerUp.Quantity}";
    }
}