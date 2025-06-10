using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private static int count;
    [SerializeField]
    private string colour;
    // Array of predefined spawn positions
    [SerializeField]
    private Vector3[] spawnPositions = new Vector3[]
    {
        //new Vector3(28, -3, -79),
        //new Vector3(-145, -3, -57),
        //new Vector3(-64, -1, -14),
    };

    public void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponentInParent<PlayerInventory>(); // check if collison is with char

        if (playerInventory != null)
        {
            gameObject.SetActive(false);
            playerInventory.CoinCollected();
            // add color to array and sort in alpha order
            count++;
        }
        if (count == 3)
            UnlockDoor();
    }
    public void UnlockDoor()
    {

    }
}
