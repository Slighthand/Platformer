using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButton : MonoBehaviour
{
    // Start is called before the first frame update
    public static void ChangeSettings(string scene)
    {
        SceneManager.LoadScene("SettingsScreen");
    }
}
