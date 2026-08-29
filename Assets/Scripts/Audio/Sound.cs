using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    [Header("Sound Settings")]
    public string clipName;
    public AudioClip clip;
    [HideInInspector] public AudioSource audioSource;
    public AudioMixerGroup mixerGroup;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    public bool loop = false;
    // this function just cleans up code on the audio manager's end
    public void SetAudioSource(AudioSource source)
    {
        audioSource = source;
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.outputAudioMixerGroup = mixerGroup;
        audioSource.loop = loop;
        audioSource.playOnAwake = false;
    }
}
