using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static int CoinCount = 0;
    public TextMeshProUGUI CoinText;

    void Update()
    {
        CoinText.text = CoinCount.ToString();
    }

    [ContextMenu("Give 100 coins please")]
    public void Give100Coins() {
        CoinCount = 100;
    }
}
