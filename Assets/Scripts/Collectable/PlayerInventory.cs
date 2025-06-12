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
    public Key key;
    private bool _keyCollected;
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
            Debug.Log("Key collected");
            _keyCollected = true;
            //key.BubbleSort(keys);
            Destroy(other.gameObject);
        }
    }

    public void AddKey(Key key)
    {
        Keys[Count++] = key.GetColour();
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
