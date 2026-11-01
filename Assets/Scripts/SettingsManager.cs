
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Volume Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private void Start()
    {
        // Restore slider positions from saved preferences
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.SetValueWithoutNotify(
                PlayerPrefs.GetFloat(
                    AudioManager.MASTER_VOLUME_KEY, 1f
                )
            );
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.SetValueWithoutNotify(
                PlayerPrefs.GetFloat(
                    AudioManager.MUSIC_VOLUME_KEY, 1f
                )
            );
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.SetValueWithoutNotify(
                PlayerPrefs.GetFloat(
                    AudioManager.SFX_VOLUME_KEY, 1f
                )
            );
        }
    }

    // Change master volume
    public void SetMasterVolume(float value)
    {
        if (!Application.isPlaying || AudioManager.Instance == null)
            return;

        AudioManager.Instance.SetMasterVolume(value);
    }

    // Change music volume
    public void SetMusicVolume(float value)
    {
        if (!Application.isPlaying || AudioManager.Instance == null)
            return;

        AudioManager.Instance.SetMusicVolume(value);
    }

    // Change sound effects volume
    public void SetSFXVolume(float value)
    {
        if (!Application.isPlaying || AudioManager.Instance == null)
            return;

        AudioManager.Instance.SetSFXVolume(value);
    }
}
