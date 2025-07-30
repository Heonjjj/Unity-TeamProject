using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill
{
    public string name;
    public string effect;
    public Skill(string name, string effect)
    {
        this.name = name;
        this.effect = effect;
    }//스킬 보여주기

    public virtual void SelectSkill(Character chracter)
    {
        Debug.Log($"{name}스킬 적용됨");
    }//스킬을 선택했을 때



}

public class MaxHpUp : Skill
{
    Character character;
    public int amount;
    public MaxHpUp(int amount):base("체력 증가",$"최대 체력이{amount} 증가합니다")
    {
        this.amount = amount;
    }
    public void IncreaseMaxHP(int amount)
    {
        character.maxHP += amount;
    }

    public override void SelectSkill(Character chracter)
    {
        base.SelectSkill(chracter);
        IncreaseMaxHP(amount);
    }
}
public class CurrentHpUp : Skill
{
    Character character;
    public int amount;
    public CurrentHpUp(int amount) : base("체력 회복", $"체력을 {amount}만큼 회복합니다")
    {
        this.amount = amount;
    }
    public override void SelectSkill(Character chracter)
    {
        base.SelectSkill(chracter);

    }
}

//각각의 스킬들의 업글레이드 작업

//스킬 UI표시
//몬스터를 다 잡고 방을 클리어했을 때
//3가지의 선택지를 화면에 띄워주고
//3가지중 하나를 마우스로 클릭하면 체력,체력회복,공격력,공격속도,이동속도가 오르게 하기

//보스 몬스터를 잡았을 때
//화살 관통,화살 추가,화살 튕기기