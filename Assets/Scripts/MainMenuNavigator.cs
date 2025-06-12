
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// navigation from main menu to game scene
public class MainMenuNavigator : LoadingGameSceneBase // inheritance to load the game scene from the main menu
{
    public string gameSceneName = "Game"; // game scene name
    public Button startGameButton;

    private void Start()
    {
        // Optional: Automatically hook up the button's click event if assigned
        if (startGameButton != null)
        {
            startGameButton.onClick.AddListener(StartGame);
        }
        else
        {
            Debug.LogWarning("Start Game Button is not assigned in the Inspector. You'll need to call StartGame() manually or via another event.");
        }
    }

    /// <summary>
    /// This method is called to initiate loading the main game scene.
    /// It uses the LoadScene method inherited from SimpleSceneLoaderBase.
    /// </summary>
    public void StartGame()
    {
        Debug.Log("Initiating game start from Main Menu...");
        // Call the base class's LoadScene method
        LoadScene(gameSceneName);
    }
}
