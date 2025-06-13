using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text nameText;
    public TMP_Text costText;
    public TMP_Text ownedText;
    public Button buyButton;

    private PowerUps powerUp;
    private ShopManager shopManager;

    public void Setup(PowerUps powerUp, ShopManager shopManager)
    {
        this.powerUp = powerUp;
        this.shopManager = shopManager;

        icon.sprite = powerUp.Icon;
        nameText.text = powerUp.Name;
        costText.text = $"Cost: {powerUp.Cost} coins";
        UpdateOwnedText();

        buyButton.onClick.AddListener(BuyPowerUp);
    }

    void BuyPowerUp()
    {
        if (shopManager.TryBuy(powerUp))
        {
            UpdateOwnedText();
        }
    }

    void UpdateOwnedText()
    {
        ownedText.text = $"Owned: x{powerUp.Owned}";
    }
}