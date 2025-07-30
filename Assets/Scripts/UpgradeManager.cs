using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    //전체 게임에 사용될 스킬들
    List<Skill> skillList = new List<Skill>();
    public List<UpgradeBtn> upgradeBtn;
    int selectSkill = 3;
    
    public void Start()
    {
        skillList.Add(new Skill("체력 증가", "체력이 1 증가합니다",1,value:1));
        skillList.Add(new Skill("공격속도증가", "공격속도가 1",2,value:1));
        skillList.Add(new Skill("데미지증가", "데미지가 1 증가합니다",3,value:1));
        skillList.Add(new Skill("사거리증가", "사거리가 1 증가합니다",4,value:1));

        for (int i = 0; i < selectSkill; i++)
        {
            int index = Random.Range(0, skillList.Count);
            Skill randomSkill = skillList[index];
            upgradeBtn[i].SetText(skillList[index].name, skillList[index].effect);
            upgradeBtn[i].SetSkill(randomSkill);
        }
    }
}
