using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static int coinCount = 0;
    public TextMeshProUGUI coinText;

    void Update()
    {
        coinText.text = coinCount.ToString();
    }
}
