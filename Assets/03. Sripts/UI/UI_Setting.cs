using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider bgmSlider;
    public Slider sfxSlider;

    void OnEnable()
    {
        if (bgmSlider != null)
            bgmSlider.onValueChanged.AddListener(OnBgmSliderChanged);
        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(OnSfxSliderChanged);
    }

    void Start()
    {
        settingsPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 설정창이 열려 있으면 닫기, 닫혀 있으면 열기
            if (settingsPanel.activeSelf)
                CloseSettings();
            else
                OpenSettings();
        }
    }
    void OnDisable()
    {
        bgmSlider.onValueChanged.RemoveListener(OnBgmSliderChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSfxSliderChanged);
    }


    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        Debug.Log("settingsPanel.activeInHierarchy = " + settingsPanel.activeInHierarchy);

        // 먼저 리스너 제거
        bgmSlider.onValueChanged.RemoveListener(OnBgmSliderChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSfxSliderChanged);

        // 슬라이더 값 설정 (이때는 이벤트 반응하지 않음)
        bgmSlider.value = AudioManager.Instance.bgmVolume;
        sfxSlider.value = AudioManager.Instance.sfxVolume;

        // 다시 리스너 추가
        bgmSlider.onValueChanged.AddListener(OnBgmSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxSliderChanged);

    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ApplySettings()
    {
        AudioManager.Instance.SetBgmVolume(bgmSlider.value);
        AudioManager.Instance.SetSfxVolume(sfxSlider.value);

        CloseSettings();
    }


    void OnBgmSliderChanged(float value)
    {
        AudioManager.Instance.SetBgmVolume(value);
    }

    void OnSfxSliderChanged(float value)
    {
        AudioManager.Instance.SetSfxVolume(value);
    }
    public void PlayClickSound() //클릭사운드
    {
        //AudioManager.Instance.PlayClickSFX();
    }
}