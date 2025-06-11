using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public string ItemType;
    public int Value;
    public int Count;

    public Items(string itemType, int value)
    {
        this. ItemType = itemType;
        this. Value = value;
        this.Count = 0;
    }

}
