
using UnityEngine;
using UnityEngine.EventSystems;

public class UISoundEffects : MonoBehaviour,
    IPointerEnterHandler,
    IPointerDownHandler
{
    [Header("UI Sounds")]
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    [Header("Volume")]
    [SerializeField, Range(0f, 1f)]
    private float hoverVolume = 0.3f;

    [SerializeField, Range(0f, 1f)]
    private float clickVolume = 0.5f;

    // Play sound when the mouse enters the UI element
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayUISound(hoverSound, hoverVolume);
    }

    // Play sound when the mouse button is pressed
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            PlayUISound(clickSound, clickVolume);
        }
    }

    private void PlayUISound(AudioClip clip, float volume)
    {
        if (clip == null || AudioManager.Instance == null)
            return;

        // Route UI sounds through the SFX mixer group
        AudioManager.Instance.PlayAudio(
            clip,
            SoundType.SFX,
            volume
        );
    }
}
