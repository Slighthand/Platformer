using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static void ReturnMenu(string scene)
    {
        SceneManager.LoadScene("StartMenu");
    }
}
