using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Awake()
    {
        foreach (var sound in SFX)
        {
            sound.SetAudioSource(gameObject.AddComponent<AudioSource>());
        }
        foreach (var sound in Music)
        {
            sound.SetAudioSource(gameObject.AddComponent<AudioSource>());
        }
        foreach (var sound in Ambient)
        {
            sound.SetAudioSource(gameObject.AddComponent<AudioSource>());
        }
    }

    public Sound[] SFX;
    public Sound[] Music;
    public Sound[] Ambient;

    private Dictionary<string, float> soundCooldowns = new Dictionary<string, float>();

    private float sfxCooldown = 0.05f;
    AudioSource audioSource;
    private Sound activeMusic;


    void OnEnable()
    {
        GameEvents.OnRequestPlaySFX += PlaySFX;
        GameEvents.OnRequestPlayMusic += PlayMusic;
        GameEvents.OnRequestPlayAmbient += PlayAmbient;
        GameEvents.OnRequestStopSFX += StopSFX;
        GameEvents.OnRequestStopMusic += StopMusic;
        GameEvents.OnRequestStopAmbient += StopAmbient;
    }

    void OnDisable()
    {
        GameEvents.OnRequestPlaySFX -= PlaySFX;
        GameEvents.OnRequestPlayMusic -= PlayMusic;
        GameEvents.OnRequestPlayAmbient -= PlayAmbient;
        GameEvents.OnRequestStopSFX -= StopSFX;
        GameEvents.OnRequestStopMusic -= StopMusic;
        GameEvents.OnRequestStopAmbient -= StopAmbient;
    }

    #region SFX
    private void PlaySFX(string clipName)
    {
        if (!CanPlaySound(clipName)) return;
        bool found = false;
        foreach (var sound in SFX)
        {
            if (sound.clipName == clipName)
            {
                found = true;

                sound.audioSource.Play();

            }
        }
        if (!found){
            Debug.LogWarning("SFX clip not found. Check your clip name.");
        }
    }
    private void PlaySFX(string clipName, float vol)
    {
        if (!CanPlaySound(clipName)) return;
        bool found = false;
        foreach (var sound in SFX)
        {
            if (sound.clipName == clipName)
            {
                found = true;

                sound.audioSource.clip = sound.clip;
                sound.audioSource.volume = vol;

                sound.audioSource.Play();
            }
        }
        if (!found){
            Debug.LogWarning("SFX clip not found. Check your clip name.");
        }
    }
    private void PlaySFX(string clipName, float vol, float pitch)
    {
        if (!CanPlaySound(clipName)) return;
        bool found = false;
        foreach (var sound in SFX)
        {
            if (sound.clipName == clipName)
            {
                found = true;

                sound.audioSource.clip = sound.clip;
                sound.audioSource.volume = vol;
                sound.audioSource.pitch = pitch;

                sound.audioSource.Play();
            }
        }
        if (!found){
            Debug.LogWarning("SFX clip not found. Check your clip name.");
        }
    }
    private void PlaySFXAtPosition(string clipName, Vector3 position)
    {
        if (!CanPlaySound(clipName)) return;
        bool found = false;
        foreach (var sound in SFX)
        {
            if (sound.clipName == clipName)
            {
                found = true;
                GameObject tempNode = new GameObject("TempAudio_" + clipName);
                tempNode.transform.position = position;
                AudioSource tempSource = tempNode.AddComponent<AudioSource>();

                tempSource.clip = sound.clip;
                tempSource.volume = sound.volume;
                tempSource.pitch = sound.pitch;
                tempSource.outputAudioMixerGroup = sound.mixerGroup;

                tempSource.spatialBlend = 1f;
                tempSource.rolloffMode = AudioRolloffMode.Linear; 
                tempSource.maxDistance = 15f; 

                tempSource.Play();
                Destroy(tempNode, sound.clip.length / sound.pitch);  
            }
        }
        if (!found){
            Debug.LogWarning("SFX clip not found. Check your clip name.");
        }
    }
    private void PlaySFXAtPosition(string clipName, int distance, Vector3 position)
    {
        if (!CanPlaySound(clipName)) return;
        bool found = false;
        foreach (var sound in SFX)
        {
            if (sound.clipName == clipName)
            {
                found = true;
                GameObject tempNode = new GameObject("TempAudio_" + clipName);
                tempNode.transform.position = position;
                AudioSource tempSource = tempNode.AddComponent<AudioSource>();

                tempSource.clip = sound.clip;
                tempSource.volume = sound.volume;
                tempSource.pitch = sound.pitch;
                tempSource.outputAudioMixerGroup = sound.mixerGroup;

                tempSource.spatialBlend = 1f;
                tempSource.rolloffMode = AudioRolloffMode.Linear; 
                tempSource.maxDistance = distance; 

                tempSource.Play();
                Destroy(tempNode, sound.clip.length / sound.pitch);  
            }
        }
        if (!found){
            Debug.LogWarning("SFX clip not found. Check your clip name.");
        }
    }
    private void PlaySFXAtPosition(string clipName, float volume, Vector3 position)
    {
        if (!CanPlaySound(clipName)) return;
        bool found = false;
        foreach (var sound in SFX)
        {
            if (sound.clipName == clipName)
            {
                found = true;
                GameObject tempNode = new GameObject("TempAudio_" + clipName);
                tempNode.transform.position = position;
                AudioSource tempSource = tempNode.AddComponent<AudioSource>();

                tempSource.clip = sound.clip;
                tempSource.volume = volume;
                tempSource.pitch = sound.pitch;
                tempSource.outputAudioMixerGroup = sound.mixerGroup;

                tempSource.spatialBlend = 1f;
                tempSource.rolloffMode = AudioRolloffMode.Linear; 
                tempSource.maxDistance = 15f; 

                tempSource.Play();
                Destroy(tempNode, sound.clip.length / sound.pitch);  
            }
        }
        if (!found){
            Debug.LogWarning("SFX clip not found. Check your clip name.");
        }
    }
    private void PlaySFXAtPosition(string clipName, float volume, float pitch, Vector3 position)
    {
        if (!CanPlaySound(clipName)) return;
        bool found = false;
        foreach (var sound in SFX)
        {
            if (sound.clipName == clipName)
            {
                found = true;
                GameObject tempNode = new GameObject("TempAudio_" + clipName);
                tempNode.transform.position = position;
                AudioSource tempSource = tempNode.AddComponent<AudioSource>();

                tempSource.clip = sound.clip;
                tempSource.volume = volume;
                tempSource.pitch = pitch;
                tempSource.outputAudioMixerGroup = sound.mixerGroup;

                tempSource.spatialBlend = 1f;
                tempSource.rolloffMode = AudioRolloffMode.Linear; 
                tempSource.maxDistance = 15f; 

                tempSource.Play();
                Destroy(tempNode, sound.clip.length / sound.pitch);  
            }
        }
        if (!found){
            Debug.LogWarning("SFX clip not found. Check your clip name.");
        }
    }
    private void PlaySFXAtPosition(string clipName, float volume, float pitch, Vector3 position, float distance)
    {
        if (!CanPlaySound(clipName)) return;
        bool found = false;
        foreach (var sound in SFX)
        {
            if (sound.clipName == clipName)
            {
                found = true;
                GameObject tempNode = new GameObject("TempAudio_" + clipName);
                tempNode.transform.position = position;
                AudioSource tempSource = tempNode.AddComponent<AudioSource>();

                tempSource.clip = sound.clip;
                tempSource.volume = volume;
                tempSource.pitch = pitch;
                tempSource.outputAudioMixerGroup = sound.mixerGroup;

                tempSource.spatialBlend = 1f;
                tempSource.rolloffMode = AudioRolloffMode.Linear; 
                tempSource.maxDistance = distance; 

                tempSource.Play();
                Destroy(tempNode, sound.clip.length / sound.pitch);  
            }
        }
        if (!found){
            Debug.LogWarning("SFX clip not found. Check your clip name.");
        }
    }
    private void StopSFX(string clipName)
    {
        if (!CanPlaySound(clipName)) return;
        bool found = false;
        foreach (var sound in SFX)
        {
            if (sound.clipName == clipName)
            {
                found = true;

                sound.audioSource.Stop();
            }
        }
        if (!found){
            Debug.LogWarning("SFX clip not found. Check your clip name.");
        }
    }
    #endregion

    #region Music
    private void PlayMusic(string clipName)
    {
        if (activeMusic != null && activeMusic.clipName == clipName) 
        {
            return;
        }
        bool found = false;
        foreach (var sound in Music)
        {
            if (sound.clipName == clipName)
            {
                found = true;
                sound.audioSource.clip = sound.clip;
                if (activeMusic != null)
                {
                    Debug.Log($"Crossfading from {activeMusic.clipName} to {sound.clipName}");
                    StartCoroutine(FadeMusic(activeMusic, sound, 1.5f));
                }
                else
                {
                    Debug.Log($"Playing music: {sound.clipName}");
                    sound.audioSource.volume = sound.volume; 
                    sound.audioSource.Play();
                }

                activeMusic = sound;
            }
        }
        if (!found){
            Debug.LogWarning("Music clip not found. Check your clip name.");
        }
    }
    private void StopMusic(string clipName)
    {
        bool found = false;
        foreach (var sound in Music)
        {
            if (sound.clipName == clipName)
            {
                found = true;

                sound.audioSource.Stop();
                Debug.Log($"Stopped music: {clipName}");
            }
        }
        if (!found){
            Debug.LogWarning("Music clip not found. Check your clip name.");
        }
    }
    #endregion
    
    #region Ambient
    private void PlayAmbient(string name)
    {
        bool found = false;
        foreach (var sound in Ambient)
        {
            if (sound.clipName == name)
            {
                found = true;
                sound.audioSource.Play();
                Debug.Log($"Playing ambient: {name}");
            }
        }
        if (!found){
            Debug.LogWarning("Ambient clip not found. Check your clip name.");
        }
    }
    private void StopAmbient(string name)
    {
        bool found = false;
        foreach (var sound in Ambient)
        {
            if (sound.clipName == name)
            {
                found = true;
                sound.audioSource.Stop();
                Debug.Log($"Stopped ambient: {name}");
            }

        }
        if (!found){
            Debug.LogWarning("Ambient clip not found. Check your clip name.");
        }
    }
    #endregion
    /////// cross fading mgmt //////
    private IEnumerator FadeMusic(Sound fadeOut, Sound fadeIn, float fadeDuration)
    {
        Debug.Log($"Starting crossfade: {fadeOut.clipName} -> {fadeIn.clipName} over {fadeDuration} seconds");
        float startingVol = fadeOut.volume;
        float targetVol = fadeIn.volume > 0f ? fadeIn.volume : 1f;
        float timeElapsed = 0f;
        fadeIn.audioSource.volume = 0f;
        // we play the second track that's going to be faded in
        // since as we're lowering the volume of the fading track, we're raising the volume of the fade in track
        fadeIn.audioSource.Play(); 
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float percentage = Mathf.Clamp01(timeElapsed / fadeDuration);

            Debug.Log($"Crossfade progress: {percentage * 100f}%");

            fadeOut.audioSource.volume = Mathf.Lerp(startingVol, 0f, percentage);
            fadeIn.audioSource.volume = Mathf.Lerp(0f, targetVol, percentage);

            Debug.Log($"FadeOut Volume: {fadeOut.audioSource.volume}, FadeIn Volume: {fadeIn.audioSource.volume}");
            yield return null;
        }
        fadeOut.audioSource.Stop();
        fadeIn.audioSource.volume = targetVol;

        fadeOut.audioSource.volume = fadeOut.volume;
        Debug.Log($"Crossfade completed: {fadeOut.clipName} -> {fadeIn.clipName}");
    }

    private bool CanPlaySound(string clipName)
    {
        if (soundCooldowns.TryGetValue(clipName, out float lastPlayedTime))
        {
            if (Time.time < lastPlayedTime + sfxCooldown)
            {
                return false;
            }
        }
        
        soundCooldowns[clipName] = Time.time;
        return true;
    }

}