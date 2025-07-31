using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBtn : MonoBehaviour
{
    Skill skill;
    Player player;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI effectText;
    public void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();

            if (player == null)
            {
                Debug.LogError("Start���� Player�� ã�� ���߽��ϴ�!");
            }
        }
    }
    public void SetText(string title,string effect)
    {
        titleText.text = title;
        effectText.text = effect;
    }
    public void SetSkill(Skill skill)
    {
        this.skill = skill;
    }
    public void SetPlayer(Player player)
    {
        this.player = player;
    }
    public void Click()
    {
        Debug.Log(skill+"����");
        Debug.Log(player);
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
                player.attackRange +=skill.value;
                Debug.Log(player.attackRange);
                break;

            case 4:
                player.moveSpeed += skill.value;
                Debug.Log(player.moveSpeed);
                break;
        }
    }//������ �� ĳ���Ϳ� ���� �� �� �ֵ���
}
