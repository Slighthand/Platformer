using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToGame : MonoBehaviour
{
    public static void StartGame(string scene)
    {
        SceneManager.LoadScene("SampleScene");
    }
}
