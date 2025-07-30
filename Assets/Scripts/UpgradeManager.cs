using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    //전체 게임에 사용될 스킬들
    List<Skill> skill = new List<Skill>();
    public List<UpgradeBtn> upgradeBtn;
    public void Start()
    {
        skill.Add(new Skill("체력 증가", "체력이 10퍼센트 증가합니다"));
        skill.Add(new Skill("공격속도증가", "공격속도가 10퍼센트 증가합니다"));
        skill.Add(new Skill("데미지증가", "데미지가 10퍼센트 증가합니다"));
        skill.Add(new Skill("사거리증가", "사거리가 10퍼센트 증가합니다"));

        upgradeBtn[0].SetText(skill[0].name, skill[0].effect);
        upgradeBtn[1].SetText(skill[1].name, skill[1].effect);
        upgradeBtn[2].SetText(skill[2].name, skill[2].effect);
    }
    
}
