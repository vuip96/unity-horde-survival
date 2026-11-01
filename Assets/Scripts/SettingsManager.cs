
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Volume Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private IEnumerator Start()
    {
        // Load saved volume preferences
        float masterVolume = PlayerPrefs.GetFloat(
            AudioManager.MASTER_VOLUME_KEY, 1f);

        float musicVolume = PlayerPrefs.GetFloat(
            AudioManager.MUSIC_VOLUME_KEY, 1f);

        float sfxVolume = PlayerPrefs.GetFloat(
            AudioManager.SFX_VOLUME_KEY, 1f);

        // Restore slider positions without triggering events
        if (masterVolumeSlider != null)
            masterVolumeSlider.SetValueWithoutNotify(masterVolume);

        if (musicVolumeSlider != null)
            musicVolumeSlider.SetValueWithoutNotify(musicVolume);

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.SetValueWithoutNotify(sfxVolume);

        // Wait one frame for audio initialization
        yield return null;

        if (AudioManager.Instance == null)
        {
            Debug.LogWarning("AudioManager instance is missing!");
            yield break;
        }

        // Explicitly restore the actual AudioMixer volumes
        AudioManager.Instance.SetMasterVolume(masterVolume);
        AudioManager.Instance.SetMusicVolume(musicVolume);
        AudioManager.Instance.SetSFXVolume(sfxVolume);
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
