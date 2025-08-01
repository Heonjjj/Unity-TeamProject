using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeUI : MonoBehaviour
{
    int selectSkill = 3;
    public List<UpgradeBtn> upgradeBtn;
    public void Start()
    {
        UpgradeImage();
    }
    public void UpgradeImage()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < selectSkill; i++)
        {
            int index = Random.Range(0, UpgradeManager.Instance.skillList.Count);
            Skill randomSkill = UpgradeManager.Instance.skillList[index];
            upgradeBtn[i].SetText(UpgradeManager.Instance.skillList[index].name, UpgradeManager.Instance.skillList[index].effect);
            upgradeBtn[i].SetSkill(randomSkill);
        }
    }
}
//�� Ŭ����� �ð� ���߰� UI�� ȭ�鿡 ����ְ� ��ư Ŭ���� ���� ��Ű���� �� UI������� �ϱ�