using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplaySaveGameName : MonoBehaviour
{
    private TextMeshProUGUI listText;
    private SaveSystem saver;

    void Start()
    {
        listText = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        UpdateText();
    }

    string UpdateText() {
        listText.text = SaveSystem.CurrentSaveGame.Name;

        return listText.text;
    }
}
