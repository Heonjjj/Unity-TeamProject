using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public string name;
    public string effect;
    int statType;
    public float value;
    public Skill(string name, string effect,int statType, float value)
    {
        this.name = name;
        this.effect = effect;
        this.statType = statType;
        this.value = value;
    }//스킬 보여주기

    public virtual void SelectSkill(Character chracter)
    {
        
    }//스킬을 선택했을 때



}



//각각의 스킬들의 업글레이드 작업

//스킬 UI표시
//몬스터를 다 잡고 방을 클리어했을 때
//3가지의 선택지를 화면에 띄워주고
//3가지중 하나를 마우스로 클릭하면 체력,체력회복,공격력,공격속도,이동속도가 오르게 하기

//보스 몬스터를 잡았을 때
//화살 관통,화살 추가,화살 튕기기