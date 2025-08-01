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
        skillList.Add(new Skill("체력 증가", "체력이 1 증가합니다", 0, value: 1));
        skillList.Add(new Skill("공격속도증가", "공격속도가 0.2", 1, value: 0.2f));
        skillList.Add(new Skill("데미지증가", "데미지가 2 증가합니다", 2, value: 2));
        skillList.Add(new Skill("사거리증가", "사거리가 1 증가합니다", 3, value: 1));
        skillList.Add(new Skill("이동속도증가", "이동속도가 1 증가합니다", 4, value: 1));
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
