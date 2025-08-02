using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class NamedSFX
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM Clips")]
    public AudioClip menuBgm;
    public AudioClip gameBgm;
    public AudioClip bossBgm;
    private AudioSource bgmSource;

    [Header("SFX Clips")]
    public List<NamedSFX> sfxClips; // Inspector���� ������ ��� ����
    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    public int sfxPoolSize = 5;

    private List<AudioSource> sfxSources = new List<AudioSource>();
    private int currentSfxIndex = 0;

    [Range(0f, 1f)] public float bgmVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // BGM Source
            AudioSource bgm = gameObject.AddComponent<AudioSource>();
            bgm.loop = true;
            bgm.playOnAwake = false;
            bgmSource = bgm;

            // SFX Pool
            for (int i = 0; i < sfxPoolSize; i++)
            {
                AudioSource sfx = gameObject.AddComponent<AudioSource>();
                sfx.playOnAwake = false;
                sfxSources.Add(sfx);
            }
            foreach (var sfx in sfxClips)
            {
                if (!sfxDict.ContainsKey(sfx.name))
                    sfxDict.Add(sfx.name, sfx.clip);
                else
                    Debug.LogWarning($"[AudioManager] �ߺ��� SFX �̸�: {sfx.name}");
            }
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // �׻� ���� �ֽ�ȭ (�ɼǿ��� �����̴��� ���� �� �ݿ��ǰ�)
        bgmSource.volume = bgmVolume;
        foreach (var sfx in sfxSources)
        {
            sfx.volume = sfxVolume;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
            PlayBGM(menuBgm);
        else if (scene.buildIndex == 1)
            PlayBGM(gameBgm);
        else if (scene.buildIndex == 2)
            PlayBGM(bossBgm);
        else if (scene.buildIndex == 3)
            PlayBGM(bossBgm);

    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return;
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxSources[currentSfxIndex].PlayOneShot(clip);
        currentSfxIndex = (currentSfxIndex + 1) % sfxPoolSize;
    }
    public void PlaySFX(string name)
    {
        if (sfxDict.ContainsKey(name))
        {
            AudioClip clip = sfxDict[name];
            sfxSources[currentSfxIndex].PlayOneShot(clip);
            currentSfxIndex = (currentSfxIndex + 1) % sfxPoolSize;
        }
        else
        {
            Debug.LogWarning($"[AudioManager] SFX '{name}' not found!");
        }
    }
    public void PlayClickSFX()
    {
        PlaySFX("Click"); // �Ǵ� Ŭ�� ���� �̸��� �°� ����
    }


    // �ܺο��� ������
    public void SetBgmVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }
}
