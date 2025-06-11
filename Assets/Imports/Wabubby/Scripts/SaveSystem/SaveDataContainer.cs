using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] // allows this class to be converted to json
public class SaveDataContainer {

    public SaveData SaveData;

    public SaveDataContainer() {
        SaveData = new SaveData();
    }

    public SaveDataContainer(SaveData saveData) {
        SaveData = saveData;
    }

    public SaveDataContainer DeepCopy() {
        // translate to and from json to make a deep copy without a billion copy() methods :)
        string json = JsonUtility.ToJson(this);
        SaveDataContainer deepCopy = JsonUtility.FromJson<SaveDataContainer>(json);

        return deepCopy;
    }

    public void AddExerciseObject() {
        return;
    }

}

[System.Serializable]
public class SaveData {

    public SaveData() {
    }

    public override string ToString() {
        return "Save Data";
    }

    public SaveData copy() {
        return new SaveData();
    }
}
