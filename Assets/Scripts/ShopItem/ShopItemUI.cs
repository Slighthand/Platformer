using UnityEngine;
using UnityEngine.UI;


public class ShopItemUI : MonoBehaviour
{
    public Image icon;
    public Text nameText;
    public Text costText;
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
       

        buyButton.onClick.AddListener(BuyPowerUp);
    }

    void BuyPowerUp()
    {
      
    }

  
}