using UnityEngine;
using UnityEngine.Events;
public class PlayerInventory : MonoBehaviour
{
    //static int scoreGoal = 8;
    //use for key
    private int _cursor;
    private string[] inventory;
    private string[] moreItems;
    private int _index;

    public void Start()
    {
        string[] inventory = new string[10];
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Collectable"))
    //    {
    //        string itemType = collision.gameObject.GetComponent<Items>().ItemType;
    //        Debug.Log("Collected an " + itemType);
    //        if (_cursor <= inventory.Length)
    //        {
    //            inventory[_cursor++] = itemType;
    //        }
    //        else
    //        {
    //            string[] moreItems = new string[inventory.Length + 10];
    //            for (_index = 0; _index < inventory.Length; _index++)
    //            {
    //                moreItems[_index] = inventory[_index];
    //            }
    //            inventory[_cursor++] = itemType;
    //        }
    //        Debug.Log(inventory);

    //        Destroy(collision.gameObject);
    //    }
    //}
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            CoinManager.coinCount++;
        }
    }

    //private void Update()
    //{
    //    if (NumberOfCoins >= scoreGoal)
    //    {
    //        UnityEngine.SceneManagement.SceneManager.LoadScene("VictoryScreen");
    //    }
    //}
    // use this to open the door after keys are collected

}
