using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string Colour;
    [SerializeField]
    private PlayerInventory playerInventory;
    public GameObject door;
    private bool doorDestroyed;

    public Key(string colour)
    {
        this.Colour = colour;
    }
    private void Start()
    {
    }
    private void Update()
    {
        if(playerInventory.count == 5 && !doorDestroyed)
        {
            doorDestroyed = true;
            Destroy(door);
        }
    }

    public static void BubbleSort(string[] keys)
    {
        for (int j = 0; j < keys.Length; j++)
        {
            bool swapped = false;
            for (int i = 0; i < keys.Length - 1; i++)
            {
                if (string.Compare(keys[i], keys[i + 1]) > 0)
                {
                    string tmp = keys[i];
                    keys[i] = keys[i + 1];
                    keys[i + 1] = tmp;
                    swapped = true;
                }
            }
            if (swapped == false)
                break;
        }
    }
}
