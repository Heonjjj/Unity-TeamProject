using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;
    public List<Skill> skillList = new List<Skill>();
    Player player;
    public void Awake()
    {
        Instance = this; 
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        skillList.Add(new Skill("ü�� ����", "ü���� 1 �����մϴ�", 0, value: 1));
        skillList.Add(new Skill("���ݼӵ�����", "���ݼӵ��� 0.2", 1, value: 0.2f));
        skillList.Add(new Skill("����������", "�������� 2 �����մϴ�", 2, value: 2));
        skillList.Add(new Skill("��Ÿ�����", "��Ÿ��� 1 �����մϴ�", 3, value: 1));
        skillList.Add(new Skill("�̵��ӵ�����", "�̵��ӵ��� 1 �����մϴ�", 4, value: 1));
    }
    public void Start()
    {
        
    }
    public void Upgrade(Skill skill)
    {
        switch (skill.statType)
        {
            case 0:
                Debug.Log(player.maxHP);
                Debug.Log(player.currentHP);
                player.maxHP += skill.value;
                player.currentHP += skill.value;

                break;

            case 1:
                player.attackSpeed += skill.value;
                Debug.Log(player.attackSpeed);
                break;

            case 2:
                player.attackPower += skill.value;
                Debug.Log(player.attackPower);
                break;

            case 3:
                player.attackRange += skill.value;
                Debug.Log(player.attackRange);
                break;

            case 4:
                player.moveSpeed += skill.value;
                Debug.Log(player.moveSpeed);
                break;
        }
    }
}
