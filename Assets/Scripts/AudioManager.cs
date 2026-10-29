
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

    // Audio mixer groups
    private AudioMixerGroup musicGroup;
    private AudioMixerGroup sfxGroup;

    // Audio mixer group names
    private const string MUSIC_GROUP = "Music";
    private const string SFX_GROUP = "SFX";

    // Exposed volume parameter names
    private const string MASTER_VOLUME = "Master";
    private const string MUSIC_VOLUME = "Music";
    private const string SFX_VOLUME = "SFX";

    private void Awake()
    {
        // Ensure that only one AudioManager exists
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

        // Find the audio mixer groups
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

        // Start playing background music
        if (backgroundMusic != null)
        {
            PlayAudio(backgroundMusic, SoundType.Music, 0.5f, true);
        }
    }

    // Convert linear volume values to decibels
    private void SetVolume(string parameterName, float volumeLevel)
    {
        volumeLevel = Mathf.Clamp(volumeLevel, 0.0001f, 1f);

        float convertedVolumeLevel =
            Mathf.Log10(volumeLevel) * 20f;

        audioMixer.SetFloat(parameterName, convertedVolumeLevel);
    }

    // Master volume control
    public void SetMasterVolume(float volumeLevel)
    {
        SetVolume(MASTER_VOLUME, volumeLevel);
    }

    // Music volume control
    public void SetMusicVolume(float volumeLevel)
    {
        SetVolume(MUSIC_VOLUME, volumeLevel);
    }

    // Sound effects volume control
    public void SetSFXVolume(float volumeLevel)
    {
        SetVolume(SFX_VOLUME, volumeLevel);
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

        // Create a new GameObject for the audio source
        GameObject audioObject = new GameObject("Audio_" + clip.name);
        audioObject.transform.SetParent(transform);

        // Add and configure AudioSource
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.volume = Mathf.Clamp01(volume);
        audioSource.loop = loop;
        audioSource.spatialBlend = 0f;

        // Select the appropriate audio mixer group
        switch (soundType)
        {
            case SoundType.Music:
                audioSource.outputAudioMixerGroup = musicGroup;
                break;

            case SoundType.SFX:
                audioSource.outputAudioMixerGroup = sfxGroup;
                break;
        }

        // Play the sound
        audioSource.Play();

        // Remove the audio object when a non-looping clip finishes
        if (!loop)
        {
            Destroy(audioObject, clip.length);
        }
    }
}
