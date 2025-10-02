using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [SerializeField] 
    private AudioSource audioSource;
    [SerializeField] private List<SoundEntry> sounds = new List<SoundEntry>();
    private Dictionary<SoundType, AudioClip> soundDictionary;
    
    private void Awake()
    {
        // Singleton básico
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Pasar la lista a un diccionario para acceso rápido
        soundDictionary = new Dictionary<SoundType, AudioClip>();
        foreach (var entry in sounds)
        {
            if (!soundDictionary.ContainsKey(entry.type) && entry.clip != null)
                soundDictionary.Add(entry.type, entry.clip);
        }
    }
    
    public void Play(SoundType type)
    {
        if (soundDictionary.TryGetValue(type, out AudioClip clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"No se encontró un sonido para el tipo: {type}");
        }
    }
    
    public void PlayMusic(SoundType type)
    {
        if (soundDictionary.TryGetValue(type, out AudioClip clip))
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"No se encontró un sonido para el tipo: {type}");
        }
    }
    
}

public enum SoundType
{
    Click,
    Jump,
    Background
}

[Serializable]
public class SoundEntry
{
    public SoundType type;
    public AudioClip clip;
}
