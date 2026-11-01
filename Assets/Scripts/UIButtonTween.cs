
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonTween : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Animation Settings")]
    [SerializeField] private float hoverScale = 1.08f;
    [SerializeField] private float clickScale = 0.95f;
    [SerializeField] private float animationDuration = 0.15f;

    private Vector3 originalScale;
    private bool isHovered;

    private void Awake()
    {
        // Save the button's original scale
        originalScale = transform.localScale;
    }

    // Mouse enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        AnimateScale(originalScale * hoverScale);
    }

    // Mouse leaves the button
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        AnimateScale(originalScale);
    }

    // Mouse button is pressed
    public void OnPointerDown(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);

        // Shrink the button briefly
        transform.localScale = originalScale * clickScale;
    }

    // Mouse button is released
    public void OnPointerUp(PointerEventData eventData)
    {
        // Return to hover size or normal size
        Vector3 targetScale = isHovered
            ? originalScale * hoverScale
            : originalScale;

        AnimateScale(targetScale);
    }

    private void AnimateScale(Vector3 targetScale)
    {
        // Prevent overlapping animations
        LeanTween.cancel(gameObject);

        LeanTween.scale(
            gameObject,
            targetScale,
            animationDuration
        )
        .setEaseOutBack()
        .setIgnoreTimeScale(true);
    }

    private void OnDisable()
    {
        // Reset the button when disabled
        LeanTween.cancel(gameObject);
        transform.localScale = originalScale;
        isHovered = false;
    }
}
