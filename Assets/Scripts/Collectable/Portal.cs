using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Door
{
    public GameObject playerg;
    public Transform player, destination;
    public PlayerInventory p1;
    public Portal(string type) : base(type)
    {
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player.position = destination.position;
        }
        if (p1.Count == 6)
            base.OnTriggerEnter2D(other);
    }

    public override void Unlock(bool unlocked)
    {
        base.Unlock(unlocked);
    }
}
