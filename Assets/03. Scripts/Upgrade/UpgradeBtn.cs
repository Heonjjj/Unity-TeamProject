using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBtn : MonoBehaviour
{
    Skill skill;
    Player player;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI effectText;

    public void SetSkill(Skill skill)
    {
        this.skill = skill;
        titleText.text = skill.name;
        effectText.text = skill.effect;
    }
    public Skill GetSkill()
    {
        return skill;
    }
    public void SetPlayer(Player player)
    {
        this.player = player;
    }
    public void Click()
    {
        UpgradeManager.Instance.Upgrade(skill);
        transform.root.gameObject.SetActive(false);
        GameManager.Instance.ChangeStage();
    }//눌렀을 때 캐릭터에 접근 할 수 있도록
}
