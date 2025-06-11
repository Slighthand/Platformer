using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DisplaySaveData : MonoBehaviour
{
    private TextMeshProUGUI listText;

    void Start()
    {
        listText = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        UpdateText();
    }

    string UpdateText()
    {
        // listText.text += $", \n{inputField.text}";
        // scrappy temp code: i will want this to be optimized and subscribe to SaveData updates
        try {
            string temp = "";

            SaveDataContainer css = SaveSystem.CurrentSlot.SaveDataCon;
            /* foreach(SaveData esd in css.SaveDatas) {
                temp += listText.text = esd.ToString() + "\n";
            } // temp = temp.TrimEnd()

            listText.text = temp;*/

            return "";
        } catch {
            listText.text = "there is no data to show";
            return listText.text;
        }
        
    }
}
