
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Canvas Groups")]
    [SerializeField] private CanvasGroup mainMenuButtons;
    [SerializeField] private CanvasGroup quitConfirmation;
    [SerializeField] private CanvasGroup settingsMenu;

    private CanvasGroup mainMenuCanvas;

    private void Awake()
    {
        // Get the Canvas Group attached to MainMenuCanvas
        mainMenuCanvas = GetComponent<CanvasGroup>();

        // Show the main menu when the game launches
        OpenMainMenu();
    }

    // Show or hide a Canvas Group
    private void SetCanvasGroup(CanvasGroup group, bool visible)
    {
        group.alpha = visible ? 1f : 0f;
        group.interactable = visible;
        group.blocksRaycasts = visible;
    }

    // Show the main menu
    public void OpenMainMenu()
    {
        SetCanvasGroup(mainMenuCanvas, true);
        SetCanvasGroup(mainMenuButtons, true);
        SetCanvasGroup(quitConfirmation, false);
        SetCanvasGroup(settingsMenu, false);
    }

    // Hide the entire main menu
    public void CloseMainMenu()
    {
        SetCanvasGroup(mainMenuCanvas, false);
    }

    // Start the game
    public void Play()
    {
        CloseMainMenu();
        GameManager.Instance.StartGame();
    }

    // Open the quit confirmation menu
    public void OpenQuitConfirmation()
    {
        SetCanvasGroup(mainMenuButtons, false);
        SetCanvasGroup(settingsMenu, false);
        SetCanvasGroup(quitConfirmation, true);
    }

    // Close the quit confirmation menu
    public void CloseQuitConfirmation()
    {
        SetCanvasGroup(quitConfirmation, false);
        SetCanvasGroup(mainMenuButtons, true);
    }

    // Open the settings menu
    public void OpenSettingsMenu()
    {
        SetCanvasGroup(mainMenuButtons, false);
        SetCanvasGroup(quitConfirmation, false);
        SetCanvasGroup(settingsMenu, true);
    }

    // Close the settings menu
    public void CloseSettingsMenu()
    {
        SetCanvasGroup(settingsMenu, false);
        SetCanvasGroup(mainMenuButtons, true);
    }

    // Quit the game
    public void Quit()
    {
        Debug.Log("Quit button pressed!");

#if UNITY_EDITOR
        // Stop Play Mode inside Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Close the application in a built game
        Application.Quit();
#endif
    }
}
