using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class SaveFileSelectDropDown : MonoBehaviour
{
    // because SaveSystem is a persistent object between scenes, button behaviour has to be instated at runtime
    private TMP_Dropdown DropDown;

    void Start() {
        DropDown = GetComponent<TMP_Dropdown>();
        DropDown.onValueChanged.AddListener(SelectSaveFile);

        SaveSystem.Instance.OnSaveGameAdded.AddListener(UpdateDropDown);

        UpdateDropDown();
    }

    private void UpdateDropDown() {
        int storedVal = DropDown.value;

        string[] options = SaveSystem.Instance.GetSaveGameNames();
        DropDown.ClearOptions();
        foreach (string option in options) {
            DropDown.options.Add(new TMP_Dropdown.OptionData(option));
        }

        DropDown.value = storedVal;
    }

    public void SelectSaveFile(int a) {
        SaveSystem.Instance.SwitchCurrentGame(DropDown.value);
        Debug.Log($"attempted switch to save file {DropDown.value}");
    }
}
