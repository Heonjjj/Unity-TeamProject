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
        skillList.Add(new Skill("Ʈ���ü�", "ȭ���� �� �߾� �߻��մϴ�", 5, value: 0));
        skillList.Add(new Skill("����", "ȭ���� �� �߾� �߻��մϴ�", 6, value: 0));
    }

    public void Upgrade(Skill skill)
    {
        if (player == null)
        {
            player = Player.Instance;

            if (player = null)
            {
                return;
            }
        }

        switch (skill.statType)
        {
            case 0:
                player.maxHP += skill.value;
                player.currentHP += skill.value;
                break;

            case 1:
                player.attackSpeed += skill.value;
                break;

            case 2:
                player.attackPower += skill.value;
                break;

            case 3:
                player.attackRange += skill.value;
                break;

            case 4:
                player.moveSpeed += skill.value;
                break;

            case 5:
                player.GetComponent<PlayerAttack>().multiShot3 = true;                
                break;

            case 6:
                player.GetComponent<PlayerAttack>().multiShot2 = true;
                break;
        }
    }
}
