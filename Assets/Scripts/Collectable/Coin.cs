using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Items
{
    [SerializeField]
    private GameObject coinPrefab; //make coin prefab

    public Coin(string itemType, int value) : base("coin", 1) { }
    
    //private static int count;
    // Array of predefined spawn positions
    [SerializeField]
    public Vector3[] spawnPositions = new Vector3[]
    {
        //new Vector3(28, -3, -79),
        //new Vector3(-145, -3, -57),
        //new Vector3(-64, -1, -14),
        //new Vector3(-160, -5, -125),
        //new Vector3(-55, 0, 30),
        //new Vector3(-89, -4, -44),
        //new Vector3(-75, 2, 33),
        //new Vector3(-178, -3, 37),
    };

    // public void OnTriggerEnter(Collider other)
    // {
    // //    PlayerInventory playerInventory = other.GetComponentInParent<PlayerInventory>(); // check if collison is with char

    // //    if (playerInventory != null)
    // //    {
    // //        gameObject.SetActive(false);
    // //        playerInventory.CoinCollected();
    // //        Spawn(count);
    // //    }
    //     CoinManager.coinCount++;
    //     Destroy(gameObject);
    // }

    public void Spawn(int count)
    {
        // Increment count and check if within spawn positions array
        if (count <= spawnPositions.Length)
        {
            GameObject clone = Instantiate(coinPrefab, spawnPositions[count], Quaternion.identity);
            clone.SetActive(true);
            Debug.Log(count);
        }
    }
}
