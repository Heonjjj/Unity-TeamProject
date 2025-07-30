using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private GameObject bossObject;

    private BossCharacter bossCharacter;

    private void Start()
    {
        bossCharacter = bossObject.GetComponent<BossCharacter>();

        bossCharacter.OnHPChanged += UpdateHPBar;
        bossCharacter.OnBossDie += OnBossDead;

        hpSlider.maxValue = bossCharacter.MaxHP;
        hpSlider.value = bossCharacter.currentHP;
    }

    private void UpdateHPBar(float currentHP)
    {
        hpSlider.value = currentHP;
    }

    private void OnBossDead()
    {
        hpSlider.gameObject.SetActive(false);

        // �� ��ȯ, Ŭ���� �г� ���� �� �߰�
    }

    private void OnDestroy()
    {
        bossCharacter.OnHPChanged -= UpdateHPBar;
        bossCharacter.OnBossDie -= OnBossDead;
    }
}
