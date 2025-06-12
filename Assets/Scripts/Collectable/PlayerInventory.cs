using UnityEngine;
using UnityEngine.Events;
public class PlayerInventory : MonoBehaviour
{

    [Header("Ruby Compas")]
    public bool hasRubyCompass = false;
    public Transform compassPointer;
    public MazeGenerator mazeGenerator;
    public Transform ruby;
    public bool noRuby = false;

    [Header("Keys")]
    public string[] Keys = new string[5];
    public int Count = 0;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            CoinManager.CoinCount += other.GetComponent<CoinPickup>().Value;
        }
        if (other.gameObject.CompareTag("Key"))
        {
            Key key = other.GetComponentInParent<Key>();
            Keys[Count++] = key.GetColour();
            //key.BubbleSort(keys);
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        if (noRuby) return;
        if (hasRubyCompass)
        {
            if (ruby == null)
            {
                ruby = FindRuby();
            }
            if (noRuby) return;
            compassPointer.LookAt(ruby);
        }
    }

    Transform FindRuby()
    {
        foreach (Transform coin in mazeGenerator.coins)
        {
            if (coin.name.Contains("Ruby"))
            {
                return coin;
            }
        }
        noRuby = true;
        return null;
    }

    //private void Update()
    //{

    //    if (NumberOfCoins >= scoreGoal)
    //    {
    //        UnityEngine.SceneManagement.SceneManager.LoadScene("VictoryScreen");
    //    }
    //}

}
