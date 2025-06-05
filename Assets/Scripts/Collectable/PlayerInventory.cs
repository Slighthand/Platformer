using UnityEngine;
using UnityEngine.Events;
public class PlayerInventory : MonoBehaviour
{
    //static int scoreGoal = 8;
    //use for key
    public int NumberOfCoins { get; private set; } // all scripts can read the value,
                                                   // but only this script can set the value

    public UnityEvent<PlayerInventory> OnCoinCollected;
    public void CoinCollected()
    {
        NumberOfCoins++;
        OnCoinCollected.Invoke(this);
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
