using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoPauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static void Pause(string scene)
    {
        SceneManager.LoadScene("PauseScreen");
    }
}
