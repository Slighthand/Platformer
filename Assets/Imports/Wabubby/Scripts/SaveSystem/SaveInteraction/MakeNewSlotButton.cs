using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(Button))]
public class MakeNewSlotButton : MonoBehaviour
{
    // because SaveSystem is a persistent object between scenes, button behaviour has to be instated at runtime
    public TMP_Dropdown DropDown;
    private Button Button;
    private SaveSystem saveSystem;
    
    public void Start() {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(AddNewSlot);
    }

    public void AddNewSlot() {
        saveSystem.AddSaveGame();
    }
}