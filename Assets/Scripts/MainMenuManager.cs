
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

        // Show only the main menu when the game launches
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

    // Open quit confirmation
    public void OpenQuitConfirmation()
    {
        SetCanvasGroup(mainMenuButtons, false);
        SetCanvasGroup(settingsMenu, false);
        SetCanvasGroup(quitConfirmation, true);
    }

    // Close quit confirmation
    public void CloseQuitConfirmation()
    {
        SetCanvasGroup(quitConfirmation, false);
        SetCanvasGroup(mainMenuButtons, true);
    }

    // Open settings menu
    public void OpenSettingsMenu()
    {
        SetCanvasGroup(mainMenuButtons, false);
        SetCanvasGroup(quitConfirmation, false);
        SetCanvasGroup(settingsMenu, true);
    }

    // Close settings menu
    public void CloseSettingsMenu()
    {
        SetCanvasGroup(settingsMenu, false);
        SetCanvasGroup(mainMenuButtons, true);
    }

    // Quit the application
    public void Quit()
    {
        Debug.Log("Quit button pressed!");
        Application.Quit();
    }
}
