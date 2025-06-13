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
            Destroy(other.gameObject);
        }
    }

    public void AddKey(Key key)
    {
        Keys[Count++] = key.GetColour();
        BubbleSort(Keys);
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
                    string temp = keys[i];
                    keys[i] = keys[i + 1];
                    keys[i + 1] = temp;
                    swapped = true;
                }
            }
            if (swapped == false)
                break;
        }
    }

    void Update()
    {
        if (noRuby) {
            compassPointer.gameObject.SetActive(false);
            return;
        }
        if (hasRubyCompass)
        {
            if (ruby == null) {
                ruby = FindRuby();
            }
            if (noRuby) return;
            compassPointer.LookAt(ruby);
        }
    }

    Transform FindRuby()
    {
        if (mazeGenerator == null) return null;
        foreach (Transform coin in mazeGenerator.coins)
        {
            if (coin == null) continue;
            if (coin.name.Contains("Ruby")) {
                return coin;
            }
        }
        noRuby = true;
        return null;
    }

}
