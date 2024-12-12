using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Background Music")]
    public AudioClip backgroundMusic;
    public float bgmVolume = 0.5f;
    private AudioSource bgmSource;

    [Header("Sound Effects")]
    public List<SFX> soundEffects = new List<SFX>();
    public float sfxVolume = 1.0f;
    private Dictionary<string, AudioClip> sfxDictionary;

    private List<AudioSource> sfxPool = new List<AudioSource>();
    public int poolSize = 10; // Number of AudioSources in the pool

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
            InitializeSFXPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudioSources()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.clip = backgroundMusic;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;
        bgmSource.playOnAwake = false;
        bgmSource.Play();

        sfxDictionary = new Dictionary<string, AudioClip>();
        foreach (SFX sfx in soundEffects)
        {
            if (!sfxDictionary.ContainsKey(sfx.name))
            {
                sfxDictionary.Add(sfx.name, sfx.clip);
            }
            else
            {
                Debug.LogWarning($"Duplicate SFX name detected: {sfx.name}. Only the first instance will be used.");
            }
        }
    }

    private void InitializeSFXPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.volume = sfxVolume;
            sfxSource.playOnAwake = false;
            sfxPool.Add(sfxSource);
        }
    }

    public void PlaySFX(string sfxName)
    {
        if (sfxDictionary.ContainsKey(sfxName))
        {
            AudioClip clip = sfxDictionary[sfxName];
            AudioSource availableSource = GetAvailableSFXSource();
            if (availableSource != null)
            {
                availableSource.clip = clip;
                availableSource.Play();
            }
            else
            {
                Debug.LogWarning("All SFX AudioSources are currently in use.");
            }
        }
        else
        {
            Debug.LogWarning($"SFX '{sfxName}' not found in AudioManager.");
        }
    }

    private AudioSource GetAvailableSFXSource()
    {
        foreach (AudioSource source in sfxPool)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    public void PlayBGM()
    {
        if (!bgmSource.isPlaying)
        {
            bgmSource.Play();
        }
    }

    [System.Serializable]
    public class SFX
    {
        public string name;
        public AudioClip clip;
    }
}
