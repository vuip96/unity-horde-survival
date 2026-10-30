
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private CanvasGroup gameOverPanel;

    private CanvasGroup inGameCanvas;

    private void Awake()
    {
        // Get the Canvas Group attached to InGameCanvas
        inGameCanvas = GetComponent<CanvasGroup>();

        // Hide the in-game UI and Game Over panel at startup
        SetCanvasGroup(inGameCanvas, false);
        SetCanvasGroup(gameOverPanel, false);
    }

    // Show or hide a Canvas Group
    private void SetCanvasGroup(CanvasGroup group, bool visible)
    {
        group.alpha = visible ? 1f : 0f;
        group.interactable = visible;
        group.blocksRaycasts = visible;
    }

    // Show the in-game UI when gameplay starts
    public void ShowInGameUI()
    {
        SetCanvasGroup(inGameCanvas, true);
    }

    // Display the Game Over screen
    public void ShowGameOverPanel()
    {
        SetCanvasGroup(inGameCanvas, true);
        SetCanvasGroup(gameOverPanel, true);
    }

    // Reload the scene and return to the main menu
    public void ReturnToMainMenu()
    {
        GameManager.Instance.ResetGame();
    }
}
