using UnityEngine;
using UnityEngine.Events;
public class PlayerInventory : MonoBehaviour
{
    public string[] keys = new string[5];
    public int count = 0;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            CoinManager.coinCount++;
        }
        if (other.gameObject.CompareTag("Key"))
        {
            Key key = other.GetComponentInParent<Key>();
            keys[count++] = key.Colour;
            //key.BubbleSort(keys);
            Destroy(other.gameObject);
        }
    }

    //private void Update()
    //{

    //    if (NumberOfCoins >= scoreGoal)
    //    {
    //        UnityEngine.SceneManagement.SceneManager.LoadScene("VictoryScreen");
    //    }
    //}

}
