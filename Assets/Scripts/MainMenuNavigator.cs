
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// navigation from main menu to game scene
public class MainMenuNavigator : LoadingGameSceneBase // inheritance to load the game scene from the main menu
{
    public string gameSceneName = "Game"; // game scene name
    public Button startGameButton;

    /// Initiate loading the main game scene
    // uses the LoadScene method inherited from SimpleSceneLoaderBase
    public void StartGame()
    {
        // Call the base class's LoadScene method
        LoadScene(gameSceneName);
    }
}
