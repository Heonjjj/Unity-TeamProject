using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    //��ü ���ӿ� ���� ��ų��
    List<Skill> skill = new List<Skill>();
    public List<UpgradeBtn> upgradeBtn;
    public void Start()
    {
        skill.Add(new Skill("ü�� ����", "ü���� 10�ۼ�Ʈ �����մϴ�"));
        skill.Add(new Skill("���ݼӵ�����", "���ݼӵ��� 10�ۼ�Ʈ �����մϴ�"));
        skill.Add(new Skill("����������", "�������� 10�ۼ�Ʈ �����մϴ�"));
        skill.Add(new Skill("��Ÿ�����", "��Ÿ��� 10�ۼ�Ʈ �����մϴ�"));

        upgradeBtn[0].SetText(skill[0].name, skill[0].effect);
        upgradeBtn[1].SetText(skill[1].name, skill[1].effect);
        upgradeBtn[2].SetText(skill[2].name, skill[2].effect);
    }
    
}
