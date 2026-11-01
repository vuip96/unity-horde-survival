
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    Music,
    SFX
}

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    public static AudioManager Instance { get; private set; }

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;

    private AudioMixerGroup musicGroup;
    private AudioMixerGroup sfxGroup;

    // Audio mixer group names
    private const string MUSIC_GROUP = "Music";
    private const string SFX_GROUP = "SFX";

    // Exposed Audio Mixer parameters
    private const string MASTER_VOLUME = "Master";
    private const string MUSIC_VOLUME = "Music";
    private const string SFX_VOLUME = "SFX";

    // PlayerPrefs keys shared with SettingsManager
    public const string MASTER_VOLUME_KEY = "MasterVolume";
    public const string MUSIC_VOLUME_KEY = "MusicVolume";
    public const string SFX_VOLUME_KEY = "SFXVolume";

    private void Awake()
    {
        // Ensure only one AudioManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Init();
    }

    private void Init()
    {
        if (audioMixer == null)
        {
            Debug.LogError("Audio Mixer is not assigned!");
            return;
        }

        // Find audio mixer groups
        AudioMixerGroup[] musicGroups =
            audioMixer.FindMatchingGroups(MUSIC_GROUP);

        AudioMixerGroup[] sfxGroups =
            audioMixer.FindMatchingGroups(SFX_GROUP);

        if (musicGroups.Length == 0 || sfxGroups.Length == 0)
        {
            Debug.LogError("Audio Mixer groups were not found!");
            return;
        }

        musicGroup = musicGroups[0];
        sfxGroup = sfxGroups[0];

        // Load saved volumes before playing audio
        LoadSavedVolumes();

        // Start background music
        if (backgroundMusic != null)
        {
            PlayAudio(backgroundMusic, SoundType.Music, 0.5f, true);
        }
    }

    // Load saved volume levels, or use defaults
    private void LoadSavedVolumes()
    {
        SetVolume(
            MASTER_VOLUME,
            PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f)
        );

        SetVolume(
            MUSIC_VOLUME,
            PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f)
        );

        SetVolume(
            SFX_VOLUME,
            PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f)
        );
    }

    // Convert linear volume to decibels
    private void SetVolume(string parameterName, float volumeLevel)
    {
        if (audioMixer == null)
            return;

        volumeLevel = Mathf.Clamp(volumeLevel, 0.0001f, 1f);

        float decibels = Mathf.Log10(volumeLevel) * 20f;

        audioMixer.SetFloat(parameterName, decibels);
    }

    // Change and save master volume
    public void SetMasterVolume(float volumeLevel)
    {
        SetVolume(MASTER_VOLUME, volumeLevel);

        PlayerPrefs.SetFloat(
            MASTER_VOLUME_KEY,
            Mathf.Clamp(volumeLevel, 0.0001f, 1f)
        );
    }

    // Change and save music volume
    public void SetMusicVolume(float volumeLevel)
    {
        SetVolume(MUSIC_VOLUME, volumeLevel);

        PlayerPrefs.SetFloat(
            MUSIC_VOLUME_KEY,
            Mathf.Clamp(volumeLevel, 0.0001f, 1f)
        );
    }

    // Change and save sound effects volume
    public void SetSFXVolume(float volumeLevel)
    {
        SetVolume(SFX_VOLUME, volumeLevel);

        PlayerPrefs.SetFloat(
            SFX_VOLUME_KEY,
            Mathf.Clamp(volumeLevel, 0.0001f, 1f)
        );
    }

    // Save preferences when closing the application
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    // Play an audio clip using the correct mixer group
    public void PlayAudio(
        AudioClip clip,
        SoundType soundType,
        float volume = 1f,
        bool loop = false)
    {
        if (clip == null)
        {
            Debug.LogWarning("AudioClip is missing!");
            return;
        }

        GameObject audioObject =
            new GameObject("Audio_" + clip.name);

        audioObject.transform.SetParent(transform);

        AudioSource audioSource =
            audioObject.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.volume = Mathf.Clamp01(volume);
        audioSource.loop = loop;
        audioSource.spatialBlend = 0f;

        switch (soundType)
        {
            case SoundType.Music:
                audioSource.outputAudioMixerGroup = musicGroup;
                break;

            case SoundType.SFX:
                audioSource.outputAudioMixerGroup = sfxGroup;
                break;
        }

        audioSource.Play();

        if (!loop)
        {
            Destroy(audioObject, clip.length);
        }
    }
}
