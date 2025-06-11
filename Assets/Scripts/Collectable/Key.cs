using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private static int _keyCount;
    [SerializeField]
    private string colour;
    public GameObject door;
    private bool doorDestroyed;

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
        }
    }
}
