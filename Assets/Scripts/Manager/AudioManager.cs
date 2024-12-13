using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [System.Serializable]
    public class SceneBGM
    {
        public string sceneName;
        public AudioClip bgmClip;
    }

    [Header("Background Music")]
    public List<SceneBGM> sceneBGMList;
    public float bgmVolume = 0.5f;
    private AudioSource bgmSource;
    private AudioClip currentBGMClip;

    [Header("Sound Effects")]
    public List<SFX> soundEffects = new List<SFX>();
    public float sfxVolume = 1.0f;
    private Dictionary<string, AudioClip> sfxDictionary;

    private List<AudioSource> sfxPool = new List<AudioSource>();
    public int poolSize = 10;

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
            InitializeSFXPool();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudioSources()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;
        bgmSource.playOnAwake = false;

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
                availableSource.loop = false;
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

    public AudioSource PlaySFXLoop(string sfxName)
    {
        if (sfxDictionary.ContainsKey(sfxName))
        {
            AudioClip clip = sfxDictionary[sfxName];
            AudioSource availableSource = GetAvailableSFXSource();
            if (availableSource != null)
            {
                availableSource.clip = clip;
                availableSource.loop = true;
                availableSource.Play();
                return availableSource;
            }
            else
            {
                Debug.LogWarning("All SFX AudioSources are currently in use.");
                return null;
            }
        }
        else
        {
            Debug.LogWarning($"SFX '{sfxName}' not found in AudioManager.");
            return null;
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string currentSceneName = scene.name;
        if (sceneBGMList.Count == 0)
        {
            Debug.LogError("Scene BGM List is empty, no BGM available.");
            return;
        }

        AudioClip targetClip = null;
        foreach (var sb in sceneBGMList)
        {
            if (sb.sceneName == currentSceneName)
            {
                targetClip = sb.bgmClip;
                break;
            }
        }

        if (targetClip == null)
        {
            Debug.LogError($"No BGM found for scene: {currentSceneName}");
            return;
        }

        if (currentBGMClip != targetClip)
        {
            currentBGMClip = targetClip;
            bgmSource.clip = currentBGMClip;
            bgmSource.Play();
        }
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
        if (!bgmSource.isPlaying && currentBGMClip != null)
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
