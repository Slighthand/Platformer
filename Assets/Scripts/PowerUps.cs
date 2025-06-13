using System;
using UnityEngine;

[Serializable]
public class PowerUps
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int Owned { get; set; }

    public PowerUps(string name, int cost, Sprite icon)
    {
        Name = name;
        if (Name == null) Name = "empty";
        Cost = cost;
        Icon = icon;
        Owned = 0;
    }

    public virtual void ApplyEffect(GameObject player) { }
}

public class SpeedBoost : PowerUps
{
    public SpeedBoost(Sprite icon) : base("Speed Boost", 10, icon) { }

    public override void ApplyEffect(GameObject player)
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.speed += 2f;
        }
    }
}

public class ExtraHeart : PowerUps
{
    public ExtraHeart(Sprite icon) : base("Extra Heart", 2, icon) { }

    public override void ApplyEffect(GameObject player)
    {
        HealthBar healthBar = GameObject.FindObjectOfType<HealthBar>();
        if (healthBar != null)
        {
            healthBar.AddExtraHeart(1);
        }
        else
        {
            Debug.LogWarning("HealthBar not found in scene.");
        }
    }
}

public class Shield : PowerUps
{
    public Shield(Sprite icon) : base("Shield", 3, icon) { }

    public override void ApplyEffect(GameObject player)
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.ActivateShield();
        }
    }
}

public class BombPowerup : PowerUps
{
    public BombPowerup(Sprite icon) : base("Bomb", 5, icon) { }

    public override void ApplyEffect(GameObject player)
    {
        // PlayerMovement movement = player.GetComponent<PlayerMovement>();
        // if (movement != null)
        // {
        //     movement.ActivateShield();
        // }
    }
}