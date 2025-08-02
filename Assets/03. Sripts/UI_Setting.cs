using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider bgmSlider;
    public Slider sfxSlider;

    void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        // 현재 볼륨을 슬라이더에 반영
        bgmSlider.value = AudioManager.Instance.bgmVolume;
        sfxSlider.value = AudioManager.Instance.sfxVolume;

        settingsPanel.SetActive(true);
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
}
