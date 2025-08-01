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
        //���Ϲ� �� �� �Ẹ��
        for (int i = 0; i < selectSkill; i++)
        {
            int index = Random.Range(0, UpgradeManager.Instance.skillList.Count);
            Skill randomSkill = UpgradeManager.Instance.skillList[index];
            if (upgradeBtn[0] == null)
            {
                upgradeBtn[i].SetText(UpgradeManager.Instance.skillList[index].name, UpgradeManager.Instance.skillList[index].effect);
                upgradeBtn[i].SetSkill(randomSkill);
            }
            else if (upgradeBtn[i] != upgradeBtn[0])
            {
                upgradeBtn[i].SetText(UpgradeManager.Instance.skillList[index].name, UpgradeManager.Instance.skillList[index].effect);
                upgradeBtn[i].SetSkill(randomSkill);
                //���� ���õ� ��ų�� ���׷��̵� ��ư[i-1].skill==randomskill�̶� ���ٸ� �ٽ� ������ ���� �־���� ��>>�ٽ� �˻�
                //while(���׷��̵� ��ư[i-1].skill==randomskill)
                //{

                //}
            }
            else if (!(upgradeBtn[0] || upgradeBtn[1] || upgradeBtn[2]))
            {
                upgradeBtn[i].SetText(UpgradeManager.Instance.skillList[index].name, UpgradeManager.Instance.skillList[index].effect);
                upgradeBtn[i].SetSkill(randomSkill);
            }
            else
            {
                i--;
            }
        }
    }
}
//�� Ŭ����� �ð� ���߰� UI�� ȭ�鿡 ����ְ� ��ư Ŭ���� ���� ��Ű���� �� UI������� �ϱ�