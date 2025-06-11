using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public static int loadingScreenIndex = 1;

    public LoadingBar loadingBar;

    private void Start() {
        
    }
    
    public void Load(int SceneIndex) {
        StartCoroutine(LoadSceneAsync(SceneIndex));
    }

    public void Load(string SceneName) {
        StartCoroutine(LoadSceneAsync(SceneName));
    }

    IEnumerator LoadSceneAsync(int SceneIndex) {
        SceneManager.LoadSceneAsync(loadingScreenIndex); // begin loading the loading screen and continue on

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneIndex); // begin loading the stats screen

        while (!asyncLoad.isDone) {
            // search for loadingBarObject until it is loaded in the loading screen

            // as an async function, loading won't load right away
            // search for the loading bar every frame (INEFFICIENT)
            // i'd rather have it make a reference right as the scene's done, but OnSceneLoaded() refuses of cooperate
            if (loadingBar == null) { loadingBar = FindObjectOfType<LoadingBar>(); }

            if (loadingBar != null) {
                loadingBar.setPercentage(asyncLoad.progress);
            }
            
            yield return null; // the operation continues before it's done
        }

    }

    IEnumerator LoadSceneAsync(string SceneName) {
        SceneManager.LoadSceneAsync(loadingScreenIndex);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);

        while (!asyncLoad.isDone) {
            if (loadingBar == null) { loadingBar = FindObjectOfType<LoadingBar>(); }
            if (loadingBar != null) { loadingBar.setPercentage(asyncLoad.progress); }
            yield return null; // the operation continues before it's done
        }

    }
}
