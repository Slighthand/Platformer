
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
        if (startGameButton != null)
        {
            startGameButton.onClick.AddListener(StartGame);
        }
        else
        {
            Debug.LogWarning("Start Game Button is not assigned in the Inspector.");
        }
    }
    public void StartGame()
    {
        Debug.Log("Initiating game start from Main Menu...");
        // Call the base class's LoadScene method
        LoadScene(gameSceneName);
    }
}
