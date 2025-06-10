

public class PowerUps 
{
    public string Name;
    public int Cost;
    // public Sprite Icon;
    public int Quantity;

    public PowerUps(string name, int cost, Sprite icon)
    {
        Name = name; 
        Cost = cost;
        Icon = icon;
        Quantity = 0;
    }

//     public virtual void ApplyEffect(GameObject player) { }
// }
