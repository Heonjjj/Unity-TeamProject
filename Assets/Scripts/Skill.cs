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
    }//��ų �����ֱ�

    public virtual void SelectSkill(Character chracter)
    {
        Debug.Log($"{name}��ų �����");
    }//��ų�� �������� ��



}

public class MaxHpUp : Skill
{
    Character character;
    public int amount;
    public MaxHpUp(int amount):base("ü�� ����",$"�ִ� ü����{amount} �����մϴ�")
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
    public CurrentHpUp(int amount) : base("ü�� ȸ��", $"ü���� {amount}��ŭ ȸ���մϴ�")
    {
        this.amount = amount;
    }
    public override void SelectSkill(Character chracter)
    {
        base.SelectSkill(chracter);

    }
}

//������ ��ų���� ���۷��̵� �۾�

//��ų UIǥ��
//���͸� �� ��� ���� Ŭ�������� ��
//3������ �������� ȭ�鿡 ����ְ�
//3������ �ϳ��� ���콺�� Ŭ���ϸ� ü��,ü��ȸ��,���ݷ�,���ݼӵ�,�̵��ӵ��� ������ �ϱ�

//���� ���͸� ����� ��
//ȭ�� ����,ȭ�� �߰�,ȭ�� ƨ���