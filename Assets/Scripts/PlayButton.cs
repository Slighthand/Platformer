using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    public static void StartGame(string scene)
    {
        SceneManager.LoadScene("Game");
    }
}
