
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Canvas Groups")]
    [SerializeField] private CanvasGroup mainMenuButtons;
    [SerializeField] private CanvasGroup quitConfirmation;

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
        SetCanvasGroup(quitConfirmation, true);
    }

    // Close quit confirmation
    public void CloseQuitConfirmation()
    {
        SetCanvasGroup(quitConfirmation, false);
        SetCanvasGroup(mainMenuButtons, true);
    }

    // Quit the application
    public void Quit()
    {
        Debug.Log("Quit button pressed!");
        Application.Quit();
    }
}
