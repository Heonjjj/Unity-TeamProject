using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM Clips")]
    public AudioClip menuBgm;
    public AudioClip gameBgm;
    public AudioClip bossBgm;

    [Header("SFX Clips")]
    public AudioClip clickSfx;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    [Range(0f, 1f)] public float bgmVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // AudioSources
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
            bgmSource.playOnAwake = false;

            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 항상 볼륨 최신화 (옵션에서 슬라이더로 조절 시 반영되게)
        bgmSource.volume = bgmVolume;
        sfxSource.volume = sfxVolume;
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
        sfxSource.PlayOneShot(clip);
    }
    public void PlayClickSFX()
    {
        PlaySFX(clickSfx);
    }

    // 외부에서 조절용
    public void SetBgmVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }
}
