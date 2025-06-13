using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadingGameSceneBase : MonoBehaviour
{
    /// Loads a specified scene by name
    public virtual void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            return;
        }

        Debug.Log($"Loading scene: {sceneName}");
        try
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log($"Successfully loaded scene");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load scene");
        }
    }
}
