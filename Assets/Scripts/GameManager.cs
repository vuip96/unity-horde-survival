
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private InGameUIManager inGameUIManager;

    private void Awake()
    {
        // Prevent duplicate GameManager instances
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Pause the game while the main menu is open
        Time.timeScale = 0f;
    }

    // Start gameplay when the PLAY button is pressed
    public void StartGame()
    {
        // Resume gameplay
        Time.timeScale = 1f;

        // Show the in-game UI
        inGameUIManager.ShowInGameUI();
    }

    // Stop gameplay and display the Game Over screen
    public void GameOver()
    {
        // Pause gameplay
        Time.timeScale = 0f;

        // Show the Game Over panel
        inGameUIManager.ShowGameOverPanel();
    }

    // Reload the current scene and return to the main menu
    public void ResetGame()
    {
        // Restore normal time before reloading
        Time.timeScale = 1f;

        // Reload the active scene
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
