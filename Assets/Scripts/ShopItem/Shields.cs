using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "PowerUps/Shield")]
public class Shield : PowerUps
{
    public override void ApplyEffect(GameObject player)
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.ActivateShield();
        }
    }
}