using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeManager : MonoBehaviour
{
    //��ü ���ӿ� ���� ��ų��
    List<Skill> skillList = new List<Skill>();
    public List<UpgradeBtn> upgradeBtn;
    int selectSkill = 3;
    
    public void Start()
    {
        skillList.Add(new Skill("ü�� ����", "ü���� 1 �����մϴ�",0,value:1));
        skillList.Add(new Skill("���ݼӵ�����", "���ݼӵ��� 0.2",1,value:0.2f));
        skillList.Add(new Skill("����������", "�������� 2 �����մϴ�",2,value:2));
        skillList.Add(new Skill("��Ÿ�����", "��Ÿ��� 1 �����մϴ�",3,value:1));
        skillList.Add(new Skill("�̵��ӵ�����", "�̵��ӵ��� 1 �����մϴ�",4,value:1));
        
        for (int i = 0; i < selectSkill; i++)
        {
            int index = Random.Range(0, skillList.Count);
            Skill randomSkill = skillList[index];
            upgradeBtn[i].SetText(skillList[index].name, skillList[index].effect);
            upgradeBtn[i].SetSkill(randomSkill);
            Debug.Log(randomSkill);
        }
    }
}
