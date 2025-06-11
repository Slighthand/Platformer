using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private static int _keyCount;
    private string[] color;
    [SerializeField]
    private string colour;
    public GameObject door;
    private bool doorDestroyed;
    private void Start()
    {
        string[] colour = new string[3];
    }
    public void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponentInParent<PlayerInventory>(); // check if collison is with char

        if (playerInventory != null)
        {
            gameObject.SetActive(false);
            //playerInventory.CoinCollected();
            // add color to array and sort in alpha order
            _keyCount++;
        }
    }

    private void Update()
    {
        if(_keyCount == 3 && !doorDestroyed)
        {
            doorDestroyed = true;
            Destroy(door);
            //print ("Congrats! You've collected the {BubbleSort(colour)} keys");
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
