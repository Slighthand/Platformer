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
    private GameObject player;

    public void Setup(PowerUps powerUp, ShopManager shopManager, GameObject player)
    {
        this.powerUp = powerUp;
        this.shopManager = shopManager;
        this.player = player;

        icon.sprite = powerUp.Icon;
        nameText.text = powerUp.Name;
        costText.text = $"Cost: {powerUp.Cost} coins";
       

        buyButton.onClick.AddListener(BuyPowerUp);
    }

    void BuyPowerUp() {
        if (shopManager.TryBuy(powerUp)) {
            // actually do effect
            print(powerUp.Name);
            if (powerUp.Name == "Shield") {
                
                player.GetComponent<PlayerMovement>().ActivateShield();
            }
            if (powerUp.Name == "Extra Heart") {
                player.GetComponent<PlayerHealth>().CurrentHealth++;
            }
            if (powerUp.Name == "Speed Boost") {
                player.GetComponent<PlayerMovement>().speed ++;
            }
            if (powerUp.Name == "Bomb") {
                print("aaaa");
                player.GetComponent<PlayerAttack>().bombCount++;
            }
        }
    }
  
}