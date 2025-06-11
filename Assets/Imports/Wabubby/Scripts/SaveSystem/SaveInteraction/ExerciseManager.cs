using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class ExerciseManager : MonoBehaviour
{
    // because SaveSystem is a persistent object between scenes, button behaviour has to be instated at runtime
    public TMP_InputField InputField;
    private Button Button;
    private SaveSystem SaveSystem;
    
    public void Start() {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(SubmitInputFieldExercise);
    }

    public void SubmitInputFieldExercise() {
        SaveDataContainer css = SaveSystem.CurrentSlot.SaveDataCon;

        // css.ExerciseSaveDatas.Add(new SaveData(InputField.text));
    }
}
