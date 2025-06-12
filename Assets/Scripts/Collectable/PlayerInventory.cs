using UnityEngine;
using UnityEngine.Events;
public class PlayerInventory : MonoBehaviour
{
    public string[] Keys = new string[5];
    public int Count = 0;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            CoinManager.CoinCount++;
        }
        if (other.gameObject.CompareTag("Key"))
        {
            Key key = other.GetComponentInParent<Key>();
            Keys[Count++] = key.GetColour();
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
